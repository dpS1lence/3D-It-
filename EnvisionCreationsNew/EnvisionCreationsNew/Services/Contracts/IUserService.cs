using EnvisionCreationsNew.Models;

namespace EnvisionCreationsNew.Services.Contracts
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserData(string userId);
    }
}
