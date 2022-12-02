using System.ComponentModel.DataAnnotations.Schema;

namespace BlenderParadise.Data.Models
{
    public class ApplicationUserProduct
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; } = null!;

        public ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
