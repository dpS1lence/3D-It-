using BlenderParadise.Constants;
using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models
{
    public class ProductModel
    {
        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NAME_MAX_LENGTH)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NUMERIC_FIELDS_MAX_LENGTH)]
        public string Polygons { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NUMERIC_FIELDS_MAX_LENGTH)]
        public string Vertices { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NUMERIC_FIELDS_MAX_LENGTH)]
        public string Geometry { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NUMERIC_FIELDS_MAX_LENGTH)]
        public string Category { get; set; } = null!;

        [Required]
        public IFormFileCollection AttachmentModel { get; set; } = null!;

        [Required]
        public IFormFileCollection CoverPhoto { get; set; } = null!;

        [Required]
        public IFormFileCollection Photos { get; set; } = null!;

        [Required]
        public IFormFileCollection PhotosZip { get; set; } = null!;
    }
}
