using BlenderParadise.Data.Models;
using BlenderParadise.Models.Product;
using BlenderParadise.Models.Profile;

namespace BlenderParadise.Services.Contracts
{
    public interface IProfileService
    {
        Task<UserProfileModel> GetUserData(string userName);

        Task<UserProfileModel> RemoveUserUploadAsync(string userId, int productId);

        Task<ApplicationUser> GetUserById(string userId);

        Task<EditProductModel> EditUserUploadAsync(int productId);

        Task<bool> EditUserUploadAsync(EditProductModel model);

        Task<bool> EditUserProfileAsync(EditProfileModel model);

        Task<EditProfileModel> EditUserProfileAsync(string profileId);

    }
}
