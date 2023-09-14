using My_E_CommerceSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace My_E_CommerceSystem.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>(); // Navigation property to CartItem

        internal string Select(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
