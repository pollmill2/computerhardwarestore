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
    public class ShoppingCartTests : UnitTestFixture
    {
        public ShoppingCartTests()
        {
                
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            var shoppingCart = new ShoppingCart(context);
            var controller = new ShoppingCartController(context, shoppingCart, mappingService.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void AddToShoppingCart_ReturnsARedirectToActionResult(int id)
        {
            var shoppingCart = new ShoppingCart(context);
            var controller = new ShoppingCartController(context, shoppingCart, mappingService.Object);

            // Act
            var result = controller.AddToShoppingCart(id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void RemoveFromShoppingCart_ReturnsARedirectToActionResult(int id)
        {
            var shoppingCart = new ShoppingCart(context);
            var controller = new ShoppingCartController(context, shoppingCart, mappingService.Object);

            // Act
            var result = controller.RemoveFromShoppingCart(id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
