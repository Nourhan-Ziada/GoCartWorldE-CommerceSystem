using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagementWithIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }
        [MinLength(3, ErrorMessage = "Name must be more than 3 char.")]
        [MaxLength(30, ErrorMessage = "Name must be less than 30 char.")]
        // may contain number number  and only contain char
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Address Must contain only char or number for street.")]
       

        public string? ProfilePicture { get; set; }
    }
}