using BlenderParadise.Constants;
using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models
{
    public class UserProductModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.PRODUCT_NAME_MIN_LENGTH)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_DESCRIPTION_MAX_LENGTH)]
        [MinLength(ValidationConstants.PRODUCT_DESCRIPTION_MIN_LENGTH)]
        public string Description { get; set; } = null!;

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string CoverPhoto { get; set; } = null!;
    }
}
