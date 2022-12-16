using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlenderParadise.Data.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] PhotoFile { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
