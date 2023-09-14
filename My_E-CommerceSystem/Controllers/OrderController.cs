using My_E_CommerceSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_E_CommerceSystem.Data;
using System.Collections.Generic;
using UserManagementWithIdentity.Models;
using static NuGet.Packaging.PackagingConstants;

namespace My_E_CommerceSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetOrder()
        {
            string userId = GetUserId();
            IEnumerable<Order>? orders = new List<Order>();
            try
            {

                if (string.IsNullOrEmpty(userId))
                    throw new Exception("You should Login First");
                orders = _dbContext.Orders
                            .Include(order => order.OrderStatus)
                            .Include(order => order.OrderDetails)
                            .Where(order =>order.UserId == userId)
                            .OrderBy(order=> order.OrderStatus)
                            .ToList() ??
                            throw new Exception("No Orders Yet");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return View("DisplayOrders", orders);
        }
        [HttpPost]
        public IActionResult Update(Order order)
        {/*
            Order orders = new Order>();
            try
            {
                orders = _dbContext.Orders
                            .Include(order => order.OrderStatus)
                            .Include(order => order.OrderDetails)
                            .FirstOrDefault(order => order.Id == _order.Id)
                                                
                          //  throw new Exception("No Orders Yet");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }*/

            if (ModelState.IsValid)
            {
                _dbContext.Update(order);
                _dbContext.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GetOrder");
        }

        public IActionResult Delete(int id)
        {
            var order = _dbContext.Orders
                             .Include(order => order.OrderStatus)
                             .Include(order => order.OrderDetails)
                             .FirstOrDefault(order => order.Id == id);
            order.OrderStatusId= 1;
            _dbContext.Update(order);
            _dbContext.SaveChanges();
            return RedirectToAction("GetOrder");

        }
        public string GetUserId()
        {
            var cliamPrinciple = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(cliamPrinciple);
            //anther way 
            return userId;
        }

    }
}
