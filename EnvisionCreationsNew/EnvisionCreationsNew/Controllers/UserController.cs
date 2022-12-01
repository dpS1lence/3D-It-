using EnvisionCreationsNew.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnvisionCreationsNew.Controllers
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

        [HttpPost]
        public async Task<IActionResult> UserEditProfile(int id)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = await _userService.RemoveUserUploadAsync(userId, id);

            return RedirectToAction("All", "UploadProduct");
        }
    }
}
