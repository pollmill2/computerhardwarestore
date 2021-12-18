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
                                ProductName = "Name",
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
