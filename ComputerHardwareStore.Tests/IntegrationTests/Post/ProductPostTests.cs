using ComputerHardwareStore.Controllers;
using ComputerHardwareStore.Models;
using ComputerHardwareStore.Tests.IntegrationTests.Base;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ComputerHardwareStore.Tests.IntegrationTests.Post
{
    public class ProductPostTests : IntegrationTestFixture
    {
        private object? _options;

        public ProductPostTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestMemoryDb").Options; // контекст
        }

        [Fact]
        public async Task AddProdcutPost_ReturnsAViewResult_WhenModelStateIsInvalid()
        {
            using (var context = new ApplicationDbContext(_options as DbContextOptions<ApplicationDbContext>)) // заполняем базу тут
            {
                var controller = new ProductController(context);
                controller.ModelState.AddModelError("ProductName", "Required");

                var model = new AddProductViewModel()
                {

                };

                // Act
                var result = await controller.AddProduct(model);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                //Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }
        [Fact]
        public async Task AddProdcutPost_ReturnsAViewResult_WhenModelStateIsValid()
        {
            using (var context = new ApplicationDbContext(_options as DbContextOptions<ApplicationDbContext>)) // заполняем базу тут
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
        }
    }
}
