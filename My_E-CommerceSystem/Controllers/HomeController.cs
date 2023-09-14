using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_E_CommerceSystem.Data;
using My_E_CommerceSystem.Models;
using My_E_CommerceSystem.ViewModels;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace My_E_CommerceSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
       /* private IConfiguration _configuration;
        private string connectionString;*/
        private readonly ApplicationDbContext _dbContext;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = new ApplicationDbContext();
            /*_configuration = Configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");*/


        }

        public IActionResult Index(string Sterm = "", int CategoryId = 0)
        {
            var products = _dbContext.Products.Include(p => p.Category)
                                    .Where(p => string.IsNullOrEmpty(Sterm) ? true : p.Name.StartsWith(Sterm))
                                    .Where(p => CategoryId == 0 ? true : p.CategoryId == CategoryId)
                                    .Select(p => new Product
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        QuantityInventory = p.QuantityInventory,
                                        PriceUnit = p.PriceUnit,
                                        Category = p.Category,
                                        CategoryId = p.Category.Id,
                                        Image = p.Image
                                    }).ToList();


            var categories = _dbContext.Categories.ToList();
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Products = products,
                Categories = categories,
                Sterm = Sterm,
                CategoryId = CategoryId,

            };

            return View(productViewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}