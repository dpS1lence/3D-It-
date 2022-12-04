using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 8)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 8)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;


        [Required]
        [StringLength(60, MinimumLength = 8)]
        public string ProfilePicture { get; set; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
