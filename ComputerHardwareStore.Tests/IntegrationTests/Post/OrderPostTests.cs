using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComputerHardwareStore.Controllers;
using ComputerHardwareStore.Entities;
using ComputerHardwareStore.Models;
using ComputerHardwareStore.Tests.IntegrationTests.Base;
using ComputerHardwareStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace ComputerHardwareStore.Tests.IntegrationTests.Post
{
    public class OrderPostTests : IntegrationTestFixture
    {
        [Fact]
        public async Task AddOrderPost_ReturnsAViewResult_WithAValidModel()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            const string url = "/Order/AddOrder";

            var model = new OrderViewModel()
            {
                //Address = "Some address",
                //FName = "Name",
                //LName = "LName",
                //Phone = "+375291111111",
                //ShoppingCart = new ShoppingCartViewModel
                //{
                //    ShoppingCart = new List<ShoppingCartItemViewModel>
                //    {
                //        new()
                //        {
                //            Product = new Product
                //            {
                //                Date = DateTime.Now,
                //                Image = "",
                //                Rating = 0,
                //                Price = 10,
                //                Specification = "8Gb RAM",
                //                CategoryId = 1
                //            },
                //            ShoppingCartId = new Guid().ToString(),
                //            ShoppingCardItemId = 1
                //        },
                //    }
                //}
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}