using BlenderParadise.Models.Product;
using BlenderParadise.Models.Profile;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace BlenderParadise.Controllers
{
	[ExcludeFromCodeCoverage]
	public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Route("UserProfile/{userName}")]
        public async Task<IActionResult> UserProfile(string userName)
        {
            var model = await _profileService.GetUserData(userName);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new ArgumentException("Invalid user.");
            }

            var model = await _profileService.RemoveUserUploadAsync(userId, id);

            TempData["message"] = $"You have successfully deleted the product.";

            return RedirectToAction(nameof(UserProfile), new { userName = User?.Identity?.Name });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProduct(int id)
        {
            var model = await _profileService.EditUserUploadAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProduct(EditProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if ((await _profileService.EditUserUploadAsync(model)).Equals(false))
            {
                throw new ArgumentException("Invalid request.");
            }

            TempData["message"] = $"You have successfully edited the product.";

            return RedirectToAction(nameof(UserProfile), new { userName = User?.Identity?.Name });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile(string id)
        {
            var model = await _profileService.EditUserProfileAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if ((await _profileService.EditUserProfileAsync(model)).Equals(false))
            {
                throw new ArgumentException("Invalid request.");
            }

            TempData["message"] = $"You have successfully edited your profile.";

            return RedirectToAction(nameof(UserProfile), new { userName = User?.Identity?.Name });

        }
    }
}
