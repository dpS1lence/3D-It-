using BlenderParadise.Models;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlenderParadise.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string userName)
        {
            var model = await _profileService.GetUserData(userName);

            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return NotFound();
            }

            var model = await _profileService.RemoveUserUploadAsync(userId, id);

            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _profileService.EditUserUploadAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductModel model)
        {
            if((await _profileService.EditUserUploadAsync(model)).Equals(false))
            {
                return NotFound();
            }

            return RedirectToAction("All", "DownloadProduct");
        }
    }
}
