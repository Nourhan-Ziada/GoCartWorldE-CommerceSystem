using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using My_E_CommerceSystem.Data;
using My_E_CommerceSystem.Models;
using System.Security.Claims;
using UserManagementWithIdentity.Models;

namespace My_E_CommerceSystem.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShoppingCartController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
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
        public IActionResult AddItem(int productId, int quantity = 1, int redirectAction = 0)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            string userId = GetUserId();
            int cartCount = 0;

            try
            {
                
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("You should Login First");
                ShoppingCart cart = GetCart(userId);
                _dbContext.ShoppingCarts.Update(cart);
                _dbContext.SaveChanges();
                var cartItem = _dbContext.CartItems.Include(cart => cart.Product).FirstOrDefault(item => item.ShoppingCartId == cart.Id && item.ProductId == productId);
                 var product = _dbContext.Products.Find(productId);

                if (cartItem is null)
                {
                    cartItem = new CartItem
                    {
                        ShoppingCartId = cart.Id,
                        ProductId = productId,
                        Quantity = quantity,
                       // UnitPrice = cartItem.Product.PriceUnit
                        UnitPrice = product.PriceUnit,
                    };
                }
                else
                {
                    cartItem.Quantity += quantity;
                }
                product.QuantityInventory -= quantity;
                _dbContext.Products.Update(product);
                _dbContext.CartItems.Update(cartItem);
                _dbContext.SaveChanges();
                transaction.Commit();
                cartCount = cart.NumberOfProducts;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);


            }
            if (redirectAction == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }
        public IActionResult RemoveItem(int productId, int quantity = 1)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            string userId = GetUserId();
            int cartCount = 0;

            try
            {

                if (string.IsNullOrEmpty(userId))
                    throw new Exception("You should Login First");
                ShoppingCart cart = GetCart(userId);
                if(cart is null)
                    throw new Exception("Invaild operation, No Cart");
                var cartItem = _dbContext.CartItems.Include(cart => cart.Product).FirstOrDefault(item => item.ShoppingCartId == cart.Id && item.ProductId == productId);
                var product = _dbContext.Products.Find(productId);
                if (cartItem is null)
                    throw new Exception("Not Products in cart");
                else if (cartItem.Quantity == 1)
                    _dbContext.CartItems.Remove(cartItem); // corner case cart isdeleted = true ;

                else
                {                     
                    cartItem.Quantity -= quantity;
                _dbContext.CartItems.Update(cartItem);


                }
                product.QuantityInventory += quantity;

                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                transaction.Commit();
                cartCount = cart.NumberOfProducts;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return RedirectToAction("GetUserCart");
        }
        public IActionResult Checkout()
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            string userId = GetUserId();
            int orderCount = 0;
            // create order and add order details 
            //move items cartDetails -> orderDetails
            // delete cartitems

            try
            {

                if (string.IsNullOrEmpty(userId))
                    throw new Exception("You should Login First");
                ShoppingCart cart = GetCart(userId) ?? throw new Exception("Invaild operation, No Cart");
                var cartItems = _dbContext.CartItems.Include(cart => cart.Product).Where(item => item.ShoppingCartId == cart.Id).ToList() ?? throw new Exception("Invaild operation, No Item Added Yet");
                var order = new Order()
                {
                     OrderDate = DateTime.UtcNow,
                     OrderStatusId = 2,
                     UserId = userId,             
                };
                 _dbContext.Orders.Add(order);
                 _dbContext.SaveChanges();
                foreach (var item in cartItems)
                {
                    OrderDetail orderItem = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                    };
                    _dbContext.OrderDetails.Update(orderItem);

                }
                _dbContext.CartItems.RemoveRange(cartItems);
                _dbContext.SaveChanges();
                orderCount = order.NumberOfProducts;
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
             return RedirectToAction("GetOrder", "Order");
        }
        public IActionResult GetTotalItemInCart()
        {
            string userId = GetUserId();
            ShoppingCart cart = GetCart(userId);

            int cartCount = 0;

            try
            {

                if (string.IsNullOrEmpty(userId))
                    throw new Exception("You should Login First");
                if (cart is null)
                    throw new Exception("Invaild operation");
                cartCount = cart.NumberOfProducts;
            }
            catch (Exception ex) { }

            return Ok(cartCount);
        }
       
        public string GetUserId()
        {
            var cliamPrinciple = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(cliamPrinciple);
            //anther way 
            /* var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

             if (userIdClaim != null)
             {
                 return userIdClaim.Value;
             }

             return null; // User ID not found*/
            return userId;
        }
        public ShoppingCart GetCart(string userId)
        {

            ShoppingCart cart = _dbContext.ShoppingCarts.Include(c=> c.CartItems).FirstOrDefault(c => c.UserId == userId);
            if (cart is null)
            {
                cart = new ShoppingCart()
                {
                    UserId = userId,
                };
            }
            return cart;
        }
        // i can remove dirven attribute and use that 
        public int GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
                 userId = GetUserId();

            int totalCount = _dbContext.CartItems.ToList().Count;

            return totalCount;
        }
        public IActionResult GetUserCart(string userId = "")
        {

            if (string.IsNullOrEmpty(userId))
                userId = GetUserId();
            var shoppingCart = _dbContext.ShoppingCarts
                                      .Include(c=> c.CartItems)
                                      .ThenInclude(item => item.Product)
                                      .ThenInclude(p=> p.Category)
                                      .FirstOrDefault(cart=> cart.UserId == userId);
            return View("DisplayCart",shoppingCart);
        }


    }
}
