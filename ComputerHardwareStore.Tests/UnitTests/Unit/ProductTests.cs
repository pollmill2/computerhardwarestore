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
        [InlineData(4, true)]
        public async Task DeleteProdcutPost_ReturnsANotFoundResult(int id, bool ready)
        {
            var controller = new ProductController(Context);

            // Act
            var result = await controller.DeleteProduct(id, ready);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(3, false, true)]
        [InlineData(3, true, false)]
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
    }
}
