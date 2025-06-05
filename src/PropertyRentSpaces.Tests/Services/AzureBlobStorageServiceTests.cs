using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using PropertyRentSpaces.Core.Models;
using PropertyRentSpaces.Infrastructure.Services;
using Xunit;

namespace PropertyRentSpaces.Tests.Services
{
    public class AzureBlobStorageServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly IMemoryCache _memoryCache;
        private readonly AzureBlobStorageService _service;

        public AzureBlobStorageServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(x => x["AzureBlobStorage:Url"])
                .Returns("https://test.blob.core.windows.net/test/test.json");
            _mockConfiguration.Setup(x => x["AzureBlobStorage:SasToken"])
                .Returns("?test-token");

            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _service = new AzureBlobStorageService(_mockConfiguration.Object, _memoryCache);
        }

        [Fact]
        public void Constructor_WithNullConfiguration_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new AzureBlobStorageService(null, _memoryCache));
        }

        [Fact]
        public void Constructor_WithNullCache_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new AzureBlobStorageService(_mockConfiguration.Object, null));
        }

        [Fact]
        public void Constructor_WithMissingBlobUrl_ThrowsArgumentNullException()
        {
            // Arrange
            var config = new Mock<IConfiguration>();
            config.Setup(x => x["AzureBlobStorage:Url"]).Returns((string)null);
            config.Setup(x => x["AzureBlobStorage:SasToken"]).Returns("test-token");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new AzureBlobStorageService(config.Object, _memoryCache));
        }

        [Fact]
        public void Constructor_WithMissingSasToken_ThrowsArgumentNullException()
        {
            // Arrange
            var config = new Mock<IConfiguration>();
            config.Setup(x => x["AzureBlobStorage:Url"]).Returns("test-url");
            config.Setup(x => x["AzureBlobStorage:SasToken"]).Returns((string)null);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new AzureBlobStorageService(config.Object, _memoryCache));
        }
    }
} 