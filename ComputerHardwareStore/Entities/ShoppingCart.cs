using ComputerHardwareStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerHardwareStore.Entities
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _applicationContext;

        public ShoppingCart(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string cardId = session.GetString("cartId") ?? Guid.NewGuid().ToString();

            session.SetString("cartId", cardId);

            return new ShoppingCart(context) { ShoppingCartId = cardId };

        }

        public void AddToCart(Product product)
        {
            var shoppingCartItem =
                _applicationContext.ShoppingCartItems.SingleOrDefault(
                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product
                };

                _applicationContext.ShoppingCartItems.Add(shoppingCartItem);
            }

            _applicationContext.SaveChanges();
        }

        public void RemoveFormCart(Product product)
        {
            var shoppingCarItem =
                _applicationContext.ShoppingCartItems.SingleOrDefault(
                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCarItem != null)
            {
                _applicationContext.ShoppingCartItems.Remove(shoppingCarItem);
            }

            _applicationContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return _applicationContext.ShoppingCartItems
                .Include(s => s.Product)
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _applicationContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _applicationContext.ShoppingCartItems.RemoveRange(cartItems);

            _applicationContext.SaveChanges();
        }

        public double GetShoppingCartTotal()
        {
            var total = _applicationContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price).Sum();

            return total;
        }
    }
}
