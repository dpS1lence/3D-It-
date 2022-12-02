using Microsoft.AspNetCore.Identity;

namespace BlenderParadise.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ProductsData = new HashSet<ApplicationUserProduct>();
        }

        public string ProfilePicture { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<ApplicationUserProduct> ProductsData { get; set; }
    }
}
