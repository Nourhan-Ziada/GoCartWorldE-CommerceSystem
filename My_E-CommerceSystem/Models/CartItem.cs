using System.ComponentModel.DataAnnotations.Schema;

namespace My_E_CommerceSystem.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public virtual Product? Product { get; set; } // Navigation property to Product
        public virtual ShoppingCart? ShoppingCart { get; set; }

    }
}
