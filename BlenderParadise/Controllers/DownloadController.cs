using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using BlenderParadise.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.IO.Compression;

namespace BlenderParadise.Controllers
{
    [Authorize]
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
            try
            {
                var fileName = await downloadService.GetNameAsync(id);

                var file = await fileService.GetFile(fileName);
                
                return file;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetDownloadZip(int id)
        {
            try
            {
                var file = await downloadService.GetZipAsync(id);

                return file;
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
