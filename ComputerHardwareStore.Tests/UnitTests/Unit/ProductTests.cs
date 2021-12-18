using ComputerHardwareStore.Controllers;
using ComputerHardwareStore.Tests.UnitTests.Base;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace ComputerHardwareStore.Tests.UnitTests.Unit
{
    public class ProductTests : UnitTestFixture
    {
        public ProductTests()
        {

        }

        [Fact]
        public async Task AddProdcutPost_ReturnsAViewResult_WhenModelStateIsInvalid()
        {
            var controller = new ProductController(Context);
            controller.ModelState.AddModelError("ProductName", "Required");

            var model = new AddProductViewModel()
            {

            };

            // Act
            var result = await controller.AddProduct(model);

            // Assert
            Assert.IsType<ViewResult>(result);
            //Assert.IsType<SerializableError>(viewResult.Value);
        }

        [Fact]
        public async Task AddProdcutPost_ReturnsARedirectToActionResult_WhenModelStateIsValid()
        {
            var controller = new ProductController(Context);

            var model = new AddProductViewModel()
            {

            };

            // Act
            var result = await controller.AddProduct(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Theory]
        [InlineData(4)]
        public async Task DeleteProdcutPost_ReturnsANotFoundResult(int id)
        {
            var controller = new ProductController(Context);

            // Act
            var result = await controller.DeleteProduct(id, true);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(2, false, true)]
        [InlineData(2, true, false)]
        [InlineData(2, true, true)]
        public async Task DeleteProdcutPost_ReturnsARedirectToActionResult(int id, bool ready, bool modelIsValid)
        {
            var controller = new ProductController(Context);

            if (!modelIsValid)
            {
                controller.ModelState.AddModelError("ProductName", "Required");
            }

            // Act
            var result = await controller.DeleteProduct(id, ready);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Theory]
        [InlineData(3)]
        public async Task DeleteProdcutPost_ReturnsAViewResult(int id)
        {
            var controller = new ProductController(Context);

            // Act
            var result = await controller.DeleteProduct(id, true);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
