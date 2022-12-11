using BlenderParadise.Constants;
using BlenderParadise.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models.Profile
{
    public class UserProfileModel
    {
        public string? Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.REGISTER_MIN_LENGTH)]
        public string? UserName { get; set; }

        public string? Bio { get; set; }

        [Required]
        public string? ProfilePhoto { get; set; }

        public List<UserProductModel> UserModels { get; set; } = null!;
    }
}
