namespace NationalDrivingLicense.Services
{
    public class AzureDidConfiguration
    {
        public string azTenantId { get; set; }
        public string azClientId { get; set; }
        public string azClientSecret { get; set; } 
        public string kvVaultUri { get; set; } 
        public string kvSigningKeyId { get; set; }
        public string kvRemoteSigningKeyId { get; set; }
        public string did { get; set; }
    }
}
