using BlenderParadise.Models;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BlenderParadise.Controllers
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
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
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

                return RedirectToAction("All", "DownloadProduct");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View();
            }
        }
    }
}
