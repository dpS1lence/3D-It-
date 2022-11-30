using Microsoft.AspNetCore.Identity;

namespace EnvisionCreationsNew.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ProductsData = new HashSet<ApplicationUserProduct>();
        }

        public byte[] ProfilePicture { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Nickname { get; set; } = null!;

        public virtual ICollection<ApplicationUserProduct> ProductsData { get; set; }
    }
}
