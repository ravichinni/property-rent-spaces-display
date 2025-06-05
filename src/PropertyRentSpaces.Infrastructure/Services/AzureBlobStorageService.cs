using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PropertyRentSpaces.Core.Models;

namespace PropertyRentSpaces.Infrastructure.Services
{
    public interface IAzureBlobStorageService
    {
        Task<List<Property>> GetPropertiesAsync();
    }

    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly string _blobUrl;
        private readonly string _sasToken;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "PropertiesCache";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

        public AzureBlobStorageService(IConfiguration configuration, IMemoryCache cache)
        {
            _blobUrl = configuration["AZURE_BLOB_URL"] 
                ?? throw new ArgumentNullException(nameof(configuration), "Azure Blob URL is not configured");
            _sasToken = configuration["AZURE_BLOB_SAS_TOKEN"] 
                ?? throw new ArgumentNullException(nameof(configuration), "Azure SAS Token is not configured");
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<List<Property>> GetPropertiesAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<Property> cachedProperties))
            {
                return cachedProperties;
            }

            try
            {
                var blobClient = new BlobClient(new Uri($"{_blobUrl}{_sasToken}"));
                
                using var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);
                memoryStream.Position = 0;

                using var reader = new StreamReader(memoryStream, Encoding.UTF8);
                var jsonContent = await reader.ReadToEndAsync();
                
                var properties = JsonConvert.DeserializeObject<List<Property>>(jsonContent) 
                    ?? throw new InvalidOperationException("Failed to deserialize properties data");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(CacheDuration)
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                _cache.Set(CacheKey, properties, cacheOptions);

                return properties;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving properties from Azure Blob Storage", ex);
            }
        }
    }
} 