using IssuerDrivingLicense.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace IssuerDrivingLicense
{
    public class IssuerService
    {
        protected readonly CredentialSettings _credentialSettings;
        protected IMemoryCache _cache;
        protected readonly ILogger<IssuerService> _log;
        private readonly DriverLicenseService _driverLicenseService;

        public IssuerService(IOptions<CredentialSettings> credentialSettings,
            IMemoryCache memoryCache,
            ILogger<IssuerService> log,
            DriverLicenseService driverLicenseService)
        {
            _credentialSettings = credentialSettings.Value;
            _cache = memoryCache;
            _log = log;
            _driverLicenseService = driverLicenseService;
        }

        public async Task<(string Token, string Error, string ErrorDescription)> GetAccessToken()
        {

            // You can run this sample using ClientSecret or Certificate. The code will differ only when instantiating the IConfidentialClientApplication
            bool isUsingClientSecret = _credentialSettings.AppUsesClientSecret(_credentialSettings);

            // Since we are using application permissions this will be a confidential client application
            IConfidentialClientApplication app;
            if (isUsingClientSecret)
            {
                app = ConfidentialClientApplicationBuilder.Create(_credentialSettings.ClientId)
                    .WithClientSecret(_credentialSettings.ClientSecret)
                    .WithAuthority(new Uri(_credentialSettings.Authority))
                    .Build();
            }
            else
            {
                X509Certificate2 certificate = _credentialSettings.ReadCertificate(_credentialSettings.CertificateName);
                app = ConfidentialClientApplicationBuilder.Create(_credentialSettings.ClientId)
                    .WithCertificate(certificate)
                    .WithAuthority(new Uri(_credentialSettings.Authority))
                    .Build();
            }

            //configure in memory cache for the access tokens. The tokens are typically valid for 60 seconds,
            //so no need to create new ones for every web request
            app.AddDistributedTokenCache(services =>
            {
                services.AddDistributedMemoryCache();
                services.AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = Microsoft.Extensions.Logging.LogLevel.Debug);
            });

            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator. 
            var scopes = new string[] { _credentialSettings.VCServiceScope };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes)
                    .ExecuteAsync();
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected
                return (string.Empty, "500", "Scope provided is not supported");
                //return BadRequest(new { error = "500", error_description = "Scope provided is not supported" });
            }
            catch (MsalServiceException ex)
            {
                // general error getting an access token
                return (String.Empty, "500", "Something went wrong getting an access token for the client API:" + ex.Message);
                //return BadRequest(new { error = "500", error_description = "Something went wrong getting an access token for the client API:" + ex.Message });
            }

            _log.LogTrace(result.AccessToken);
            return (result.AccessToken, string.Empty, string.Empty);
        }

        public string GetRequestHostName(HttpRequest request)
        {
            string scheme = "https";// : this.Request.Scheme;
            string originalHost = request.Headers["x-original-host"];
            string hostname = "";
            if (!string.IsNullOrEmpty(originalHost))
                hostname = string.Format("{0}://{1}", scheme, originalHost);
            else hostname = string.Format("{0}://{1}", scheme, request.Host);
            return hostname;
        }

        public bool IsMobile(HttpRequest request)
        {
            string userAgent = request.Headers["User-Agent"];

            if (userAgent.Contains("Android") || userAgent.Contains("iPhone"))
                return true;
            else
                return false;
        }

        public async Task<IssuanceRequestPayload> GetIssuanceRequestPayloadAsync(HttpRequest request, HttpContext context)
        {
            var payload = new IssuanceRequestPayload();
            var length = 4;
            var pinMaxValue = (int)Math.Pow(10, length) - 1;
            var randomNumber = RandomNumberGenerator.GetInt32(1, pinMaxValue);
            var newpin = string.Format("{0:D" + length.ToString() + "}", randomNumber);

            payload.Issuance.Pin.Length = 4;
            payload.Issuance.Pin.Value = newpin;
            payload.Issuance.CredentialsType = "MyDrivingLicense";
            payload.Issuance.Manifest = _credentialSettings.CredentialManifest;

            var host = GetRequestHostName(request);
            payload.Callback.State = Guid.NewGuid().ToString();
            payload.Callback.Url = string.Format("{0}:/api/issuer/issuanceCallback", host);
            payload.Callback.Headers.ApiKey = "OPTIONAL API-KEY for ISSUANCE CALLBACK API";

            payload.Registration.ClientName = "Verifiable Credential NDL Sample";
            payload.Authority = _credentialSettings.IssuerAuthority;

            var driverLicense = await _driverLicenseService.GetDriverLicense(context.User.Identity.Name);

            payload.Issuance.Claims.Name = $"{driverLicense.FirstName} {driverLicense.Name}  {driverLicense.UserName}";
            payload.Issuance.Claims.Details = $"Type: {driverLicense.LicenseType} IssuedAt: {driverLicense.IssuedAt.ToString("yyyy-MM-dd")}";

            return payload;
        }
    }
}
