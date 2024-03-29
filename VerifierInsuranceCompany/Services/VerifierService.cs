using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using VerifierInsuranceCompany.Services;

namespace VerifierInsuranceCompany;

public class VerifierService
{
    protected readonly CredentialSettings _credentialSettings;
    protected readonly ILogger<VerifierService> _log;

    public VerifierService(IOptions<CredentialSettings> credentialSettings,
        ILogger<VerifierService> log)
    {
        _credentialSettings = credentialSettings.Value;
        _log = log;
    }

    public VerifierRequestPayload GetVerifierRequestPayload(HttpRequest request)
    {
        var payload = new VerifierRequestPayload();

        var host = GetRequestHostName(request);
        payload.Callback.State = Guid.NewGuid().ToString();
        payload.Callback.Url = $"{host}/api/verifier/presentationCallback";
        payload.Callback.Headers.ApiKey = _credentialSettings.VcApiCallbackApiKey;

        payload.Registration.ClientName = "NDL Iso18013 Drivers License";
        payload.Authority = _credentialSettings.VerifierAuthority;

        var requestedCredentials = new RequestedCredentials
        {
            CrendentialsType = "Iso18013DriversLicense",
            Purpose = "So we can see that you a veriable credentials NDL"
        };
        requestedCredentials.AcceptedIssuers.Add(_credentialSettings.IssuerAuthority);
        payload.RequestedCredentials.Add(requestedCredentials);

        return payload;
    }

    public async Task<(string Token, string Error, string ErrorDescription)> GetAccessToken()
    {
        // You can run this sample using ClientSecret or Certificate. The code will differ only when instantiating the IConfidentialClientApplication
        var isUsingClientSecret = _credentialSettings.AppUsesClientSecret(_credentialSettings);

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
            var certificate = _credentialSettings.ReadCertificate(_credentialSettings.CertificateName);
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

        AuthenticationResult? result = null;
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
            return (string.Empty, "500", "Something went wrong getting an access token for the client API:" + ex.Message);
            //return BadRequest(new { error = "500", error_description = "Something went wrong getting an access token for the client API:" + ex.Message });
        }

        _log.LogTrace("{AccessToken}", result.AccessToken);
        return (result.AccessToken, string.Empty, string.Empty);
    }

    public string GetRequestHostName(HttpRequest request)
    {
        var scheme = "https";// : Request.Scheme;
        var originalHost = request.Headers["x-original-host"];
        if (!string.IsNullOrEmpty(originalHost))
        {
            return $"{scheme}://{originalHost}";
        }
        else
        {
            return $"{scheme}://{request.Host}";
        }
    }
}
