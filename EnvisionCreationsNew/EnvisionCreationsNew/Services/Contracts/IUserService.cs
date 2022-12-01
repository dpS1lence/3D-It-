using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;

namespace EnvisionCreationsNew.Services.Contracts
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserData(string userId);

        Task<UserProfileModel> RemoveUserUploadAsync(string userId, int productId);

        Task<ApplicationUser> GetUserById(string userId);

        Task<UserProfileModel> EditUserUploadAsync(string userId, int productId);
    }
}
