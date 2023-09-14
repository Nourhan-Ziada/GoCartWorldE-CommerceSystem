using My_E_CommerceSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_E_CommerceSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        [NotMapped] // Mark this property as dynamic
        public decimal TotalPrice
        {
            get
            {
                return OrderDetails.Sum(item => (int)(item.UnitPrice * (item.Quantity)));
            }
        }
        [NotMapped] // Mark this property as dynamic
        public int NumberOfProducts
        {
            get
            {
                return OrderDetails.Sum(item => item.Quantity);
            }
        }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();  // Navigation property to CartItem
       // public int UserId { get; set; }
        public string UserId { get; set; }

       // public virtual User? User { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }



    }
}
