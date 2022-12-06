using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlenderParadise.Data.Models
{
    public class Product
    {
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

        [ForeignKey(nameof(Content))]
        public int ContentId { get; set; }
        public Content? Content { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = null!;
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
