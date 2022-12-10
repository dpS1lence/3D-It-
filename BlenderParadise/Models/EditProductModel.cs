using BlenderParadise.Constants;
using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models
{
    public class EditProductModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_NAME_MAX_LENGTH)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; } = null!;
    }
}
