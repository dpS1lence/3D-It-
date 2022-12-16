using BlenderParadise.Areas.Admin.Services;
using BlenderParadise.Areas.Admin.Services.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlenderParadise.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        private readonly IProfileService _profileService;
        private readonly IControlService _controlService;

        public ProductController(IProfileService profileService, IControlService controlService)
        {
            _profileService = profileService;
            _controlService = controlService;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId, string userId)
        {
            if (userId == null)
            {
                throw new ArgumentException("Invalid user.");
            }

            var model = await _profileService.RemoveUserUploadAsync(userId, productId);


            await _controlService.AddPenalty(userId);

            TempData["message"] = $"You have successfully deleted {model.UserName}'s product (with id {productId}).";

            return RedirectToAction("All", "Product", new { area = "" });
        }
    }
}
