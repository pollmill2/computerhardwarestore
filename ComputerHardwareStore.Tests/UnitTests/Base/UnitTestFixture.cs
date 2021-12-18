using ComputerHardwareStore.Entities;
using ComputerHardwareStore.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
        protected DbContextOptions<ApplicationDbContext> Option;
        protected ApplicationDbContext Context;

        public UnitTestFixture()
        {

            Option = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestMemoryDb").Options;

            Context = new ApplicationDbContext(Option);

            var category = new Category
            {
                Id = 1,
                CategoryName = "Ноутбуки"
            };
            Context.Add(category);

            var product = new Product
            {
                Id = 2,
                ProductName = "WithoutOrder",
                Date = DateTime.Now,
                Image = "",
                Rating = 0,
                Price = 10,
                Specification = "8Gb RAM",
                CategoryId = 1,
            };
            Context.Add(product);

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
            Context.Add(order);

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Run(() =>
            {
                return Task.FromResult(true);
            });
        }
    }
}
