using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_E_CommerceSystem.Data;
using My_E_CommerceSystem.Models;

namespace My_E_CommerceSystem.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
           var Categories = _dbContext.Categories.ToList();

            return View("DisplayCategories",Categories);
        }
        public IActionResult Create()
        {
            return View("CreateCategory");
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(category);
                _dbContext.SaveChanges();
                return Redirect("Index");
            }
            return View("Create", category); ;

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _dbContext.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {

            if (ModelState.IsValid)
            {
                _dbContext.Update(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Update");
        }
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            _dbContext.Remove(category);

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
