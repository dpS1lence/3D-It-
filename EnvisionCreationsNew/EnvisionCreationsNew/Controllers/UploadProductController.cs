using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnvisionCreationsNew.Controllers
{
    public class UploadProductController : Controller
    {
        private readonly IUploadService uploadService;

        public UploadProductController(IUploadService _uploadService)
        {
            uploadService = _uploadService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

                await uploadService.UploadProductAsync(model, userId);

                return RedirectToPage("All");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View();
            }
        }
    }
}
