using BlenderParadise.Data.Models;
using BlenderParadise.Models;

namespace BlenderParadise.Services.Contracts
{
    public interface IProfileService
    {
        Task<UserProfileModel> GetUserData(string userId);

        Task<UserProfileModel> RemoveUserUploadAsync(string userId, int productId);

        Task<ApplicationUser> GetUserById(string userId);

        Task<EditProductModel> EditUserUploadAsync(int productId);

        Task<bool> EditUserUploadAsync(EditProductModel model);
    }
}
