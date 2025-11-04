using Moq;
using Xunit;
using SAS_Key_Generation;
using SAS_Key_Generation.Helpers;

namespace SAS_Key_Generation.Tests
{
    public class SASKeyGenerationTests
    {
        [Fact]
        public void GenerateSASKey_ReturnsMockedUrl_WhenStorageHelperIsMocked()
        {
            // Arrange
            var mockHelper = new Mock<IStorageHelper>();
            mockHelper
                .Setup(h => h.GenerateContainerSasToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns("https://mockaccount.blob.core.windows.net/container?mockedsastoken");

            var generator = new SASKeyGeneration(mockHelper.Object, "dummy-conn", "test-container");

            // Act
            var result = generator.GenerateSASKey();

            // Assert
            Assert.Equal("https://mockaccount.blob.core.windows.net/container?mockedsastoken", result);
            mockHelper.Verify(h => h.GenerateContainerSasToken(It.IsAny<string>(), "test-container", It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GenerateSASKey_ThrowsApplicationException_WhenHelperFails()
        {
            // Arrange
            var mockHelper = new Mock<IStorageHelper>();
            mockHelper
                .Setup(h => h.GenerateContainerSasToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new InvalidOperationException("Storage error"));

            var generator = new SASKeyGeneration(mockHelper.Object, "conn", "container");

            // Act & Assert
            var ex = Assert.Throws<ApplicationException>(() => generator.GenerateSASKey());
            Assert.Contains("Storage error", ex.Message);
        }
    }
}
