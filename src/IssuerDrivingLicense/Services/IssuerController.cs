using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Net.Http.Headers;
using IssuerDrivingLicense.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace IssuerDrivingLicense
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssuerController : ControllerBase
    {
        protected readonly CredentialSettings _credentialSettings;
        protected IMemoryCache _cache;
        protected readonly ILogger<IssuerController> _log;
        private readonly IssuerService _issuerService;
        private readonly HttpClient _httpClient;

        public IssuerController(IOptions<CredentialSettings> credentialSettings,
            IMemoryCache memoryCache,
            ILogger<IssuerController> log,
            IssuerService issuerService,
            IHttpClientFactory httpClientFactory)
        {
            _credentialSettings = credentialSettings.Value;
            _cache = memoryCache;
            _log = log;
            _issuerService = issuerService;
            _httpClient = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// This method is called from the UI to initiate the issuance of the verifiable credential
        /// </summary>
        /// <returns>JSON object with the address to the presentation request and optionally a QR code and a state value which can be used to check on the response status</returns>
        [HttpGet("/api/issuer/issuance-request")]
        public async Task<ActionResult> IssuanceRequestAsync()
        {
            try
            {
                var payload = await _issuerService.GetIssuanceRequestPayloadAsync(Request, HttpContext);
                try
                {
                    var (Token, Error, ErrorDescription) = await _issuerService.GetAccessToken();
                    if (string.IsNullOrEmpty(Token))
                    {
                        _log.LogError("failed to acquire accesstoken: {Error} : {ErrorDescription}", Error, ErrorDescription);
                        return BadRequest(new { error = Error, error_description = ErrorDescription });
                    }

                    var defaultRequestHeaders = _httpClient.DefaultRequestHeaders;
                    defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    HttpResponseMessage res = await _httpClient.PostAsJsonAsync(
                        _credentialSettings.ApiEndpoint, payload);

                    var response = await res.Content.ReadFromJsonAsync<IssuanceResponse>();

                    if (response == null)
                    {
                        return BadRequest(new { error = "400", error_description = "no response from VC API" });
                    }

                    if (res.StatusCode == HttpStatusCode.Created)
                    {
                        _log.LogTrace("succesfully called Request API");

                        if (payload.Issuance.Pin.Value != null)
                        {
                            response.Pin = payload.Issuance.Pin.Value;
                        }

                        response.Id = payload.Callback.State;

                        var cacheData = new CacheData
                        {
                            Status = IssuanceConst.NotScanned,
                            Message = "Request ready, please scan with Authenticator",
                            Expiry = response.Expiry.ToString()
                        };
                        _cache.Set(payload.Callback.State, JsonSerializer.Serialize(cacheData));

                        return Ok(response);
                    }
                    else
                    {
                        _log.LogError("Unsuccesfully called Request API");
                        return BadRequest(new { error = "400", error_description = "Something went wrong calling the API: " + response });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = "400", error_description = "Something went wrong calling the API: " + ex.Message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "400", error_description = ex.Message });
            }
        }

        /// <summary>
        /// This method is called by the VC Request API when the user scans a QR code and accepts the issued Verifiable Credential
        /// </summary>
        [AllowAnonymous]
        [HttpPost("/api/issuer/issuanceCallback")]
        public async Task<ActionResult> IssuanceCallback()
        {
            string content = await new System.IO.StreamReader(Request.Body).ReadToEndAsync();
            var issuanceResponse = JsonSerializer.Deserialize<IssuanceCallbackResponse>(content);

            try
            {
                //there are 2 different callbacks. 1 if the QR code is scanned (or deeplink has been followed)
                //Scanning the QR code makes Authenticator download the specific request from the server
                //the request will be deleted from the server immediately.
                //That's why it is so important to capture this callback and relay this to the UI so the UI can hide
                //the QR code to prevent the user from scanning it twice (resulting in an error since the request is already deleted)
                if (issuanceResponse?.Code == IssuanceConst.RequestRetrieved)
                {
                    var cacheData = new CacheData
                    {
                        Status = IssuanceConst.RequestRetrieved,
                        Message = "QR Code is scanned. Waiting for issuance...",
                    };
                    _cache.Set(issuanceResponse.State, JsonSerializer.Serialize(cacheData));
                }

                if (issuanceResponse?.Code == IssuanceConst.IssuanceSuccessful)
                {
                    var cacheData = new CacheData
                    {
                        Status = IssuanceConst.IssuanceSuccessful,
                        Message = "Credential successfully issued",
                    };
                    _cache.Set(issuanceResponse.State, JsonSerializer.Serialize(cacheData));
                }

                if (issuanceResponse?.Code == IssuanceConst.IssuanceError)
                {
                    var cacheData = new CacheData
                    {
                        Status = IssuanceConst.IssuanceError,
                        Payload = issuanceResponse.Error?.Code,
                        //at the moment there isn't a specific error for incorrect entry of a pincode.
                        //So assume this error happens when the users entered the incorrect pincode and ask to try again.
                        Message = issuanceResponse.Error?.Message
                    };
                    _cache.Set(issuanceResponse.State, JsonSerializer.Serialize(cacheData));
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "400", error_description = ex.Message });
            }
        }

        /// <summary>
        /// this function is called from the UI polling for a response from the AAD VC Service.
        /// when a callback is recieved at the issuanceCallback service the session will be updated
        /// this method will respond with the status so the UI can reflect if the QR code was scanned and with the result of the issuance process
        /// </summary>
        [HttpGet("/api/issuer/issuance-response")]
        public ActionResult IssuanceResponse()
        {
            try
            {
                //the id is the state value initially created when the issuanc request was requested from the request API
                //the in-memory database uses this as key to get and store the state of the process so the UI can be updated
                string state = this.Request.Query["id"];
                if (string.IsNullOrEmpty(state))
                {
                    return BadRequest(new { error = "400", error_description = "Missing argument 'id'" });
                }
                CacheData? value = null;
                if (_cache.TryGetValue(state, out string buf))
                {
                    value = JsonSerializer.Deserialize<CacheData>(buf);

                    Debug.WriteLine("check if there was a response yet: " + value);
                    return new ContentResult { ContentType = "application/json", Content = JsonSerializer.Serialize(value) };
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "400", error_description = ex.Message });
            }
        }
    }
}
