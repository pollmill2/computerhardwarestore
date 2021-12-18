using System;
using System.Collections.Generic;
using System.Net;
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
    public class PostForAllTests : IntegrationTestFixture
    {
        [Theory]
        [InlineData("/Product/AddProduct")]
        [InlineData("/Product/DeleteProduct")]
        [InlineData("/Product/EditProduct")]
        [InlineData("/Order/AddOrder")]
        [InlineData("/ShoppingCart/Index")]
        public async Task Post_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var model = new OrderViewModel()
            {

            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Some")]
        public async Task Post_EndpointsReturnErrorAndNotFoundContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var model = new OrderViewModel()
            {

            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(response.Content.Headers.ContentType);
        }
    }
}