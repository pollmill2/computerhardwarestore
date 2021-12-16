using System.Net;
using System.Threading.Tasks;
using ComputerHardwareStore.Tests.IntegrationTests.Base;
using Xunit;

namespace ComputerHardwareStore.Tests.IntegrationTests.Get
{
    public class GetForAllTests : BaseTests
    {
        [Theory]
        [InlineData("/")]
        [InlineData("/Privacy")]
        [InlineData("/Error")]
        [InlineData("/Product/Index")]
        [InlineData("/Product/AddProduct")]
        [InlineData("/Product/DeleteProduct")]
        [InlineData("/Product/EditProduct")]
        [InlineData("/Order/Index")]
        [InlineData("/Order/AddOrder")]
        [InlineData("/ShoppingCart/Index")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
        [Theory]
        [InlineData("/some")]
        public async Task Get_EndpointsReturnErrorAndNotFoundContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(response.Content.Headers.ContentType);
        }
    }
}