using Microsoft.AspNetCore.Identity;

namespace EnvisionCreationsNew.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ProductsData = new HashSet<ApplicationUserProduct>();
        }
        public virtual ICollection<ApplicationUserProduct> ProductsData { get; set; } = null!;
    }
}
