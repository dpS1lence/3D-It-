﻿using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace BlenderParadise.Controllers
{
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class DownloadController : Controller
    {
        private readonly IDownloadService downloadService;
        private readonly IFileService fileService;

        public DownloadController(IDownloadService downloadService, IFileService fileService)
        {
            this.downloadService = downloadService;
            this.fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> GetDownloadModel(int id)
        {
            var fileName = await downloadService.GetNameAsync(id);

            var file = await fileService.GetFile(fileName);

            return file;
        }

        [HttpPost]
        public async Task<IActionResult> GetDownloadZip(int id)
        {
            var file = await downloadService.GetZipAsync(id);

            return file;
        }
    }
}
