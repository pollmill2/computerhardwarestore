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
    public class ShoppingCartEntityTests : UnitTestFixture
    {
        public ShoppingCartEntityTests() 
        {
                
        }

        [Fact]
        public void GetShoppingCartItems_ReturnsList()
        {
            var shoppingCart = new ShoppingCart(context);

            // Act
            var result = shoppingCart.GetShoppingCartItems();

            // Assert
            Assert.IsType<List<ShoppingCartItem>>(result);
        }

        [Fact]
        public void GetShoppingCartTotal_ReturnsList()
        {
            var shoppingCart = new ShoppingCart(context);

            // Act
            var result = shoppingCart.GetShoppingCartTotal();

            double excepted = 10;

            // Assert
            Assert.Equal(excepted, result);
        }
    }
}
