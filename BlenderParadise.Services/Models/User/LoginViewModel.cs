using BlenderParadise.Constants;
using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models.User
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(ValidationConstants.PASSWORD_MAX_LENGTH)]
        [MinLength(ValidationConstants.PASSWORD_MIN_LENGTH)]
        public string Password { get; set; } = null!;
    }
}
