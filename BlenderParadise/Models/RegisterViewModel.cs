using BlenderParadise.Constants;
using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(ValidationConstants.REGISTER_MAX_LENGTH, MinimumLength = ValidationConstants.REGISTER_MIN_LENGTH)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.REGISTER_MAX_LENGTH, MinimumLength = ValidationConstants.REGISTER_MIN_LENGTH)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.PRODUCT_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; } = null!;

        [Required]
        public string ProfilePicture { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.REGISTER_MAX_LENGTH, MinimumLength = ValidationConstants.REGISTER_MIN_LENGTH)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
