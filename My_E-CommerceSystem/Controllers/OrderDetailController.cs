using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_E_CommerceSystem.Data;
using My_E_CommerceSystem.Models;
using UserManagementWithIdentity.Models;

namespace My_E_CommerceSystem.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderDetailController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
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
        public IActionResult GetDetails(int orderId)
        {
            string userId = GetUserId();
            IEnumerable<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("You should Login First");

                orderDetails = _dbContext.Orders
                    .Include(order => order.OrderDetails)
                    .ThenInclude(detail => detail.Product)
                    .Where(order => order.UserId == userId && order.Id == orderId)
                    .SelectMany(order => order.OrderDetails)
                    .ToList();

                if (orderDetails.Count() == 0)
                    throw new Exception("No Orders Yet");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View("GetOrderDetails", orderDetails);
        }

        public string GetUserId()
        {
            var cliamPrinciple = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(cliamPrinciple);
            return userId;
        }


    }
}
