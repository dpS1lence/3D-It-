using BlenderParadise.Models.Product;
using BlenderParadise.Models.Profile;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlenderParadise.Controllers
{
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
            try
            {
                var model = await _profileService.GetUserData(userName);

                return View(model);
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

                if(userId == null)
                {
                    return NotFound("User not found");
                }

                var model = await _profileService.RemoveUserUploadAsync(userId, id);

                return RedirectToAction(nameof(UserProfile), new { userName = User?.Identity?.Name });
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            try
            {
                var model = await _profileService.EditUserUploadAsync(id);

                return View(model);
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                if ((await _profileService.EditUserUploadAsync(model)).Equals(false))
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(UserProfile), new { userName = User?.Identity?.Name });
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(string id)
        {
            try
            {
                var model = await _profileService.EditUserProfileAsync(id);

                return View(model);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                if ((await _profileService.EditUserProfileAsync(model)).Equals(false))
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(UserProfile), new { userName = User?.Identity?.Name });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
