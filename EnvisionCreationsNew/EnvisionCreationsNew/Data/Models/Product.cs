using System.ComponentModel.DataAnnotations;

namespace EnvisionCreationsNew.Data.Models
{
    public class Product
    {
        public Product()
        {
            ApplicationUsersProducts = new HashSet<ApplicationUserProduct>();

            ProductsContent = new HashSet<ProductContent>();

            ProductPhotos = new HashSet<ProductPhoto>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(26)]
        public string Name { get; set; } = null!;

        [MaxLength(256)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(26)]
        public int Polygons { get; set; }

        [Required]
        [MaxLength(26)]
        public int Vertices { get; set; }

        [Required]
        [MaxLength(26)]
        public int Geometry { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public byte[] Photo { get; set; } = null!;

        public ICollection<ApplicationUserProduct> ApplicationUsersProducts { get; set; } = null!;

        public ICollection<ProductContent> ProductsContent { get; set; } = null!;

        public ICollection<ProductPhoto> ProductPhotos { get; set; } = null!;
    }
}
