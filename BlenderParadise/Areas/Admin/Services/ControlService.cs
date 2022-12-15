using BlenderParadise.Areas.Admin.Services.Contracts;
using BlenderParadise.Data.Models;
using BlenderParadise.Models.Product;
using BlenderParadise.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;

namespace BlenderParadise.Areas.Admin.Services
{
    public class ControlService : IControlService
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ControlService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task AddPenalty(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("invalid userId");
            }

            user.Penalties = "One or more products have been deleted from your profile, be careful next time!";

            await _repository.SaveChangesAsync();
        }
    }
}
