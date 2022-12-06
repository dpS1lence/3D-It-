using Microsoft.AspNetCore.Identity;

namespace BlenderParadise.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ProductsData = new HashSet<Product>();
        }

        public string ProfilePicture { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<Product> ProductsData { get; set; }
    }
}
