using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace BlenderParadise.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IDownloadService downloadService;

        public DownloadController(IDownloadService downloadService)
        {
            this.downloadService = downloadService;
        }

        [HttpPost]
        [RequestSizeLimit(209715200)]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<IActionResult> GetDownloadModel(int id)
        {
            var file = await downloadService.DownloadModelAsync(id);

            if (file == null)
            {
                return NotFound();
            }
            else
            {
                return file;
            }
        }

        [HttpPost]
        [RequestSizeLimit(209715200)]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<IActionResult> GetDownloadZip(int id)
        {
            var file = await downloadService.DownloadZipAsync(id);

            if (file == null)
            {
                return NotFound();
            }
            else
            {
                return file;
            }
        }
    }
}
