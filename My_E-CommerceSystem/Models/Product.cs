using Humanizer.Localisation;
using My_E_CommerceSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace My_E_CommerceSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]{3,20}$", ErrorMessage = "Name must be only char. and range char between 5 and 10")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Must be only a Postive Number")]
        public int QuantityInventory { get; set; }

        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = " Must be only a Postive Number")]
        public float PriceUnit { get; set;}
        [RegularExpression(@"\w*\.(jpg|png)", ErrorMessage = "Image must end with .png OR .jpg")]
        public string? Image { get; set; }


        public int? CategoryId { get; set; }
        public virtual Category? Category  { get; set; }

        public ICollection<OrderDetail> OrderDetails{ get; set; } = new HashSet<OrderDetail>(); // Navigation property to CartItem

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>(); // Navigation property to CartItem

    }
}
