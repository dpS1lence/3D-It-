using System.ComponentModel.DataAnnotations.Schema;

namespace EnvisionCreationsNew.Data.Models
{
    public class ProductPhoto
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        public Photo? Photo { get; set; }
    }
}
