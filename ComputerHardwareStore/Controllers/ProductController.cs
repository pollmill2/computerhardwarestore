using System;
using System.Linq;
using System.Threading.Tasks;
using ComputerHardwareStore.Models;
using ComputerHardwareStore.Entities;
using ComputerHardwareStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComputerHardwareStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public ProductController(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var products = _applicationContext.Products.ToList();
            return View(products);
        }

        /*
        public IActionResult CategoryList()
        {
            return View(_applicationContext.Categories.ToList());
        }*/

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_applicationContext.Categories, "Id", "CategoryName");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            ViewBag.Categories = new SelectList(_applicationContext.Categories, "Id", "CategoryName");
            if (ModelState.IsValid)
            {
                if (_applicationContext.Products.Any(x => x.ProductName == model.ProductName))
                {
                    ModelState.AddModelError("", "Продукт уже есть в списке");
                    return View();
                }

                var product = new Product()
                {
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    Image = model.Image,
                    Price = model.Price,
                    Date = model.Date,
                    Specification = model.Specification
                };
                _applicationContext.Add(product);
                await _applicationContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            ViewBag.Categories = new SelectList(_applicationContext.Categories, "Id", "CategoryName");
            var product = _applicationContext.Products.Find(id);

            if (product != null)
            {
                var addProductViewModel = new AddProductViewModel()
                {
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Image = product.Image,
                    Price = product.Price,
                    Date = product.Date,
                    Specification = product.Specification
                };
                return View(addProductViewModel);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, AddProductViewModel model)
        {
            ViewBag.Categories = new SelectList(_applicationContext.Categories, "Id", "CategoryName");

            if (ModelState.IsValid)
            {
                var product = _applicationContext.Products.Find(id);

                product.ProductName = model.ProductName;
                product.CategoryId = model.CategoryId;
                product.Image = model.Image;
                product.Price = model.Price;
                product.Date = model.Date;
                product.Specification = model.Specification;

                _applicationContext.Update(product);

                await _applicationContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var product = _applicationContext.Products.Include(x => x.Category).SingleOrDefault(c => c.Id == id);

            if (product != null)
            {
                var addProductViewModel = new AddProductViewModel()
                {
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Image = product.Image,
                    Price = product.Price,
                    Date = product.Date,
                    Specification = product.Specification
                };
                ViewBag.CategoryName = product.Category.CategoryName;
                return View(addProductViewModel);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id, bool ready)
        {
            if (ModelState.IsValid)
            {
                if (!_applicationContext.Products.Any(x => x.Id == id))
                {
                    return NotFound();
                }

                if (ready)
                {
                    var items = _applicationContext.ShoppingCartItems.Include(e => e.Product);
                    var orders = _applicationContext.Orders.Include(x => x.CardItems).ThenInclude(c => c.Product);
                    if (!orders.Any(x => x.CardItems.Any(x => x.Product.Id == id)))
                    {
                        _applicationContext.RemoveRange(items.Where(x => x.Product.Id == id));
                        _applicationContext.Remove(_applicationContext.Products.Find(id));
                        await _applicationContext.SaveChangesAsync();
                    }
                    else
                    {
                        var product = _applicationContext.Products.Include(x => x.Category).SingleOrDefault(c => c.Id == id);

                        var addProductViewModel = new AddProductViewModel()
                        {
                            ProductName = product.ProductName,
                            CategoryId = product.CategoryId,
                            Image = product.Image,
                            Price = product.Price,
                            Date = product.Date,
                            Specification = product.Specification
                        };

                        ViewBag.CategoryName = product.Category.CategoryName;
                        ModelState.AddModelError("", "Нельзя удалить товар, который находится в одном из заказов.");
                        return View(addProductViewModel);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
