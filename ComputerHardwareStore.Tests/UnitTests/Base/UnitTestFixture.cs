using AutoMapper;
using ComputerHardwareStore.Entities;
using ComputerHardwareStore.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ComputerHardwareStore.Tests.UnitTests.Base
{
    public class UnitTestFixture : IDisposable
    {
        protected DbContextOptions<ApplicationDbContext> optoins;
        protected ApplicationDbContext context;
        protected Mock<IMapper> mappingService;

        public UnitTestFixture()
        {

            optoins = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            context = new ApplicationDbContext(optoins);

            mappingService = new Mock<IMapper>();

            var category = new Category
            {
                Id = 1,
                CategoryName = "Ноутбуки"
            };
            context.Add(category);

            var product = new Product
            {
                Id = 2,
                ProductName = "WithoutOrder",
                Date = DateTime.Now,
                Image = "",
                Rating = 0,
                Price = 100,
                Specification = "8Gb RAM",
                CategoryId = 1,
            };
            context.Add(product);

            var order = new Order
            {
                Address = "Some address",
                Phone = "+375291111111",
                FName = "Name",
                LName = "Name",
                CardItems =
                    new List<ShoppingCartItem>
                    {
                        new ShoppingCartItem
                        {
                            Product = new Product {
                                Id = 3,
                                ProductName = "InOrder",
                                Date = DateTime.Now,
                                Image = "",
                                Rating = 0,
                                Price = 10,
                                Specification = "8Gb RAM",
                                CategoryId = 1,
                            },
                        }
                    }
            };
            context.Add(order);

            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Run(() =>
            {
                context.Database.EnsureDeleted();
                return Task.FromResult(true);
            });
        }
    }
}
