using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ProductsData = new HashSet<Product>();
        }
        [Required]
        public string ProfilePicture { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public virtual ICollection<Product> ProductsData { get; set; }
    }
}
