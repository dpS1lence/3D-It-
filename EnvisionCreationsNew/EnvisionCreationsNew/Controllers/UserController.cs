using BlenderParadise.Models;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlenderParadise.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> UserProfile(string userName)
        {
            var model = await _userService.GetUserData(userName);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserProfile(int id)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = await _userService.RemoveUserUploadAsync(userId, id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var model = await _userService.EditUserUploadAsync(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, EditProductModel model)
        {
            await _userService.EditUserUploadAsync(model);

            return RedirectToAction("All", "DownloadProduct");
        }
    }
}
