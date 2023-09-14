
using My_E_CommerceSystem.Models;
using My_E_CommerceSystem.Models;

namespace My_E_CommerceSystem.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Category>? Categories { get; set; }

        public string? Sterm { get; set; } = string.Empty;
        public int?  CategoryId { get; set; } 
     

        public ProductViewModel()
        {
            
        }

      
    }
}
