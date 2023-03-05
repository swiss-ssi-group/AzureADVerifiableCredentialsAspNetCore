using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;

namespace VerifierInsuranceCompany.Services;

public class CacheData
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("expiry")]
    public string Expiry { get; set; } = string.Empty;
    [JsonPropertyName("payload")]
    public string Payload { get; set; } = string.Empty;
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("details")]
    public string Details { get; set; } = string.Empty;

    public static void AddToCache(string key, IDistributedCache cache, CacheData cacheData)
    {
        var cacheExpirationInDays = 1;

        var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(cacheExpirationInDays));

        cache.SetString(key, System.Text.Json.JsonSerializer.Serialize(cacheData), options);
    }

    public static CacheData? GetFromCache(string key, IDistributedCache cache)
    {
        var item = cache.GetString(key);
        if (item != null)
        {
            return System.Text.Json.JsonSerializer.Deserialize<CacheData>(item);
        }

        return null;
    }
}
