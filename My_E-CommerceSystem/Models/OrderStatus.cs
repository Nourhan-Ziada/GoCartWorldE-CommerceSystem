using System.ComponentModel.DataAnnotations;

namespace My_E_CommerceSystem.Models
{
    public class OrderStatus
    {
        [Key]
        public int StatusCode { get; set; }
        [MaxLength(20)]
        public string? StatusName { get; set; }

        public static implicit operator OrderStatus(int v)
        {
            throw new NotImplementedException();
        }
    }
}
