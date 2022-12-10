using BlenderParadise.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlenderParadise.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NAME_MAX_LENGTH)]
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.PRODUCT_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NUMERIC_FIELDS_MAX_LENGTH)]
        public int Polygons { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NUMERIC_FIELDS_MAX_LENGTH)]
        public int Vertices { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NUMERIC_FIELDS_MAX_LENGTH)]
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
