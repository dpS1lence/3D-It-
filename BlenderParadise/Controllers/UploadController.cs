using BlenderParadise.Constants;
using BlenderParadise.Models.Product;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BlenderParadise.Controllers
{
    [Authorize]
	[ExcludeFromCodeCoverage]
	public class UploadController : Controller
    {
        private readonly IUploadService uploadService;

        public UploadController(IUploadService _uploadService)
        {
            uploadService = _uploadService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = ValidationConstants.MULTIPART_BODY_LENGTH_LIMIT)]
        [RequestSizeLimit(ValidationConstants.MULTIPART_BODY_LENGTH_LIMIT)]
        public async Task<IActionResult> Upload(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new ArgumentException("Invalid user.");
            }

            if ((await uploadService.UploadProductAsync(model, userId)).Equals(false))
            {
                throw new ArgumentException("Invalid request.");
            }

            TempData["message"] = $"You have successfully uploaded a product.";

            return RedirectToAction("UserProfile", "Profile", new { userName = User?.Identity?.Name });

        }
    }
}
