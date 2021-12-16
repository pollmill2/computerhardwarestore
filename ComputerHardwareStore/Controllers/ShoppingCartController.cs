using ComputerHardwareStore.Models;
using ComputerHardwareStore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ComputerHardwareStore.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ShoppingCart _shoppingCart;
        private readonly IMapper _mapper;

        public ShoppingCartController(ApplicationDbContext applicationDbContext, ShoppingCart shoppingCart, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _shoppingCart = shoppingCart;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _mapper.Map<List<ShoppingCartItemViewModel>>(_shoppingCart.GetShoppingCartItems())
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
