using System.ComponentModel.DataAnnotations.Schema;

namespace My_E_CommerceSystem.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }

    }
}
