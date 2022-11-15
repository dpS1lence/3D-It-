using System.ComponentModel.DataAnnotations.Schema;

namespace EnvisionCreationsNew.Data.Models
{
    public class ProductContent
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey(nameof(Content))]
        public int ContentId { get; set; }
        public Content? Content { get; set; }
    }
}
