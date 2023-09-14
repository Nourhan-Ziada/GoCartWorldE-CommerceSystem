using System.Reflection.Metadata;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Immutable;

namespace My_E_CommerceSystem.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        [NotMapped] // Mark this property as dynamic
        public decimal TotalPrice
        {
            get
            {
                // Calculate the total price by summing up the product prices for all items in the cart
                return CartItems.Sum(item => (int)(item.Product.PriceUnit * (item.Quantity)));
            }
        }
        [NotMapped] // Mark this property as dynamic
        public int NumberOfProducts
        {
            get
            {
                // Calculate the number of products in the cart by summing up the quantities
                return CartItems.ToList().Count;
            }
        }
        public bool IsDeleted { get; set; } = false;

        [Required]
        public string UserId { get; set; }

        /* public int UserId { get; set; }
         public User? User { get; set; }*/

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>(); // Navigation property to CartItem
        

       

    }
}

