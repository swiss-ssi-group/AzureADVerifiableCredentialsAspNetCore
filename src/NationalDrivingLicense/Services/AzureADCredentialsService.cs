using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NationalDrivingLicense.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NationalDrivingLicense
{
    public class AzureADCredentialsService
    {
        private readonly IConfiguration _configuration;
        private readonly DriverLicenseService _driverLicenseService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly AzureDidConfiguration _azureDidConfiguration;

        public AzureADCredentialsService(IConfiguration configuration,
            DriverLicenseService driverLicenseService,
            IHttpClientFactory clientFactory,
            IOptions<AzureDidConfiguration> optionsAzureADConfiguration)
        {
            _configuration = configuration;
            _driverLicenseService = driverLicenseService;
            _clientFactory = clientFactory;
            _azureDidConfiguration = optionsAzureADConfiguration.Value;
        }

        public async Task<string> GetDriverLicenseCredential(string username)
        {
            if (!await _driverLicenseService.HasIdentityDriverLicense(username))
            {
                throw new ArgumentException("user has no valid driver license");
            }

            var driverLicense = await _driverLicenseService.GetDriverLicense(username);

            if (!string.IsNullOrEmpty(driverLicense.DriverLicenseCredentials))
            {
                return driverLicense.DriverLicenseCredentials;
            }
            IDictionary<string, string> credentialValues = new Dictionary<String, String>() {
                {"Issued At", driverLicense.IssuedAt.ToString()},
                {"Name", driverLicense.Name},
                {"First Name", driverLicense.FirstName},
                {"Date of Birth", driverLicense.DateOfBirth.Date.ToString()},
                {"License Type", driverLicense.LicenseType}
            };

            await CreateMattrVc(credentialValues);

            driverLicense.DriverLicenseCredentials = string.Empty;
            await _driverLicenseService.UpdateDriverLicense(driverLicense);

            return "https://damienbod.com";
        }

        private async Task CreateMattrVc(IDictionary<string, string> credentialValues)
        {
            HttpClient client = _clientFactory.CreateClient();
            var accessToken = "";// todo
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            // var did = await CreateMattrDid(client);
            // var vc = // TODO create
             
        }

    }
}
