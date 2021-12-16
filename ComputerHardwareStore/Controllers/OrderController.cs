using ComputerHardwareStore.Entities;
using ComputerHardwareStore.Models;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerHardwareStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(ApplicationDbContext applicationContext, ShoppingCart shoppingCart)
        {
            _applicationContext = applicationContext;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderList()
        {
            return View(_applicationContext.Orders.Include(c => c.CardItems).ThenInclude(s => s.Product).ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var orderViewModel = new OrderViewModel()
            {
                ShoppingCart = _shoppingCart
            };

            return View(orderViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    Address = model.Address,
                    Phone = model.Phone,
                    FName = model.FName,
                    LName = model.LName,
                    CardItems = _shoppingCart.GetShoppingCartItems()
                };

                _applicationContext.Add(order);
                await _applicationContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); ;
            }

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var orderViewModel = new OrderViewModel()
            {
                ShoppingCart = _shoppingCart
            };

            return View(orderViewModel);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (ModelState.IsValid)
            {
                var items = _applicationContext.Orders.Include(e => e.CardItems).FirstOrDefault(x => x.Id == id);
                _applicationContext.RemoveRange(items.CardItems);
                _applicationContext.Remove(items);
                await _applicationContext.SaveChangesAsync();
            }

            return RedirectToAction("OrderList");
        }

        /*[Authorize(Roles = "admin")]
        public IActionResult ListOrder()
        {
            return View();
        }*/
    }
}
