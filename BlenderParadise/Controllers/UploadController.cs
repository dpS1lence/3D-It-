﻿using BlenderParadise.Models;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BlenderParadise.Controllers
{
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

                if(userId == null)
                {
                    return NotFound();
                }

                if((await uploadService.UploadProductAsync(model, userId)).Equals(false))
                {
                    return NotFound();
                }

                return RedirectToAction("UserProfile", "Profile", new { userName = User?.Identity?.Name });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View();
            }
        }
    }
}
