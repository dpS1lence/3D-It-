using EnvisionCreationsNew.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EnvisionCreationsNew.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> UserProfile(string userId)
        {
            var model = await _userService.GetUserData(userId);

            return View(model);
        }
    }
}
