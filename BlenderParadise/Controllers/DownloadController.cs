using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.IO.Compression;

namespace BlenderParadise.Controllers
{
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
                var fileName = await downloadService.DownloadModelAsync(id);

                string filePath = fileService.GetPath(fileName);

                return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetDownloadZip(int id)
        {
            try
            {
                var file = await downloadService.DownloadZipAsync(id);

                return file;
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
