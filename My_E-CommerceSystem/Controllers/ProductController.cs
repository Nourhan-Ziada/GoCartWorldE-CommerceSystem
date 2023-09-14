using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_E_CommerceSystem.Data;
using My_E_CommerceSystem.Models;
using System.IO;

namespace My_E_CommerceSystem.Controllers
{

    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(p => p.Category)
                       .Select(p => new Product
                       {
                           Id = p.Id,
                           Name = p.Name,
                           QuantityInventory = p.QuantityInventory,
                           PriceUnit = p.PriceUnit,
                           Category = p.Category,
                           Image = p.Image
                       })
                       .ToList();

            return View(products);
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            var product = _dbContext.Products.Include(p => p.Category)
                                   .Select(p => new Product
                                   {
                                       Id = p.Id,
                                       Name = p.Name,
                                       QuantityInventory = p.QuantityInventory,
                                       PriceUnit = p.PriceUnit,
                                       Category = p.Category,
                                       Image = p.Image
                                   })
                                   .FirstOrDefault(p=>p.Id == id);

            return View(product);
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Categories = _dbContext.Categories.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(product);
                _dbContext.SaveChanges();
                return Redirect("Index");
            }
            ViewBag.Categories = _dbContext.Categories.ToList();

            return View("Create",product); ;

        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _dbContext.Products.Find(id);
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {

            if (ModelState.IsValid)
            {
                _dbContext.Update(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("update");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.Find(id);
            _dbContext.Remove(product);

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public int GetAvailability(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            return product.QuantityInventory;
        }


    }
}
