using ComputerHardwareStore.Controllers;
using ComputerHardwareStore.Entities;
using ComputerHardwareStore.Tests.UnitTests.Base;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public void AddProdcutGet_ReturnsAViewResult()
        {
            var controller = new ProductController(context);

            // Act
            var result = controller.AddProduct();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddProdcutPost_ReturnsAViewResult_WhenModelStateIsInvalid()
        {
            var controller = new ProductController(context);
            controller.ModelState.AddModelError("ProductName", "Required");

            var model = new AddProductViewModel()
            {

            };

            // Act
            var result = await controller.AddProduct(model);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddProdcutPost_ReturnsARedirectToActionResult_WhenModelStateIsValid()
        {
            var controller = new ProductController(context);

            var model = new AddProductViewModel()
            {

            };

            // Act
            var result = await controller.AddProduct(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void DeleteProdcutGet_ReturnsAViewResult_WhenProductIsExist()
        {
            var controller = new ProductController(context);

            // Act
            var result = controller.DeleteProduct(3);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void DeleteProdcutGet_ReturnsAViewResult_WhenProductIsNotExist()
        {
            var controller = new ProductController(context);

            // Act
            var result = controller.DeleteProduct(4);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(4)]
        public async Task DeleteProdcutPost_ReturnsANotFoundResult(int id)
        {
            var controller = new ProductController(context);

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
            var controller = new ProductController(context);

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
            var controller = new ProductController(context);

            // Act
            var result = await controller.DeleteProduct(id, true);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditProdcutGet_ReturnsAViewResult_WhenProductIsExist()
        {
            var controller = new ProductController(context);

            // Act
            var result = controller.EditProduct(3);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditProdcutGet_ReturnsAViewResult_WhenProductIsNotExist()
        {
            var controller = new ProductController(context);

            // Act
            var result = controller.EditProduct(4);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditProdcutPost_ReturnsAViewResult_WhenModelStateIsInvalid()
        {
            var controller = new ProductController(context);
            controller.ModelState.AddModelError("ProductName", "Required");

            var model = new AddProductViewModel()
            {

            };

            // Act
            var result = await controller.EditProduct(2, model);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditProdcutPost_ReturnsARedirectToActionResult_WhenModelStateIsValid()
        {
            var controller = new ProductController(context);

            var model = new AddProductViewModel()
            {

            };

            // Act
            var result = await controller.EditProduct(2, model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
