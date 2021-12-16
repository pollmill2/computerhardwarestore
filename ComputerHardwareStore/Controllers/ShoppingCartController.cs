using ComputerHardwareStore.Models;
using ComputerHardwareStore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ComputerHardwareStore.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ApplicationDbContext applicationDbContext, ShoppingCart shoppingCart)
        {
            _applicationDbContext = applicationDbContext;
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(sCVM);
        }

        [Authorize]
        public RedirectToActionResult AddToShoppingCart(int id)
        {
            var selectedProduct = _applicationDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public RedirectToActionResult RemoveFromShoppingCart(int id)
        {
            var selectedProduct = _applicationDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFormCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }
    }
}
