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
            try
            {
                if (userId == null)
                {
                    return NotFound("User not found");
                }

                var model = await _profileService.RemoveUserUploadAsync(userId, productId);

                try
                {
                    await _controlService.AddPenalty(userId);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                TempData["message"] = $"You have successfully deleted {model.UserName}'s product (with id {productId}).";

                return RedirectToAction("All", "Product", new { area = ""});
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
