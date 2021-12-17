using ComputerHardwareStore.Entities;
using ComputerHardwareStore.Models;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System;

namespace ComputerHardwareStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly ShoppingCart _shoppingCart;
        private readonly IMapper _mapper;

        public OrderController(ApplicationDbContext applicationContext, ShoppingCart shoppingCart, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _shoppingCart = shoppingCart;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _applicationContext.Orders.Include(c => c.CardItems)
                .ThenInclude(s => s.Product)
                .ToList();

            return View(orders);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddOrder()
        {
            var shoppingCart = new ShoppingCartViewModel()
            {
                ShoppingCart =  _mapper.Map<List<ShoppingCartItemViewModel>>(_shoppingCart.GetShoppingCartItems())
            };
            var orderViewModel = new OrderViewModel()
            {
                ShoppingCart = shoppingCart
            };

            return View(orderViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Address = model.Address,
                    Phone = model.Phone,
                    FName = model.FName,
                    LName = model.LName,
                    CardItems = _shoppingCart.GetShoppingCartItems()
                };

                _applicationContext.Add(order);
                await _applicationContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            var shoppingCart = new ShoppingCartViewModel()
            {
                ShoppingCart = _mapper.Map<List<ShoppingCartItemViewModel>>(_shoppingCart.GetShoppingCartItems())
            };
            var orderViewModel = new OrderViewModel()
            {
                ShoppingCart = shoppingCart
            };

            return View(orderViewModel);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (ModelState.IsValid)
            {
                var items = _applicationContext.Orders
                    .Include(e => e.CardItems)
                    .FirstOrDefault(x => x.Id == id);
                
                _applicationContext.RemoveRange(items.CardItems);
                _applicationContext.Remove(items);
                
                await _applicationContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}