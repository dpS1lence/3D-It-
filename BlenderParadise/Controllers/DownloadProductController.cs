using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace BlenderParadise.Controllers
{
    public class DownloadProductController : Controller
    {
        private readonly IDownloadService downloadService;

        public DownloadProductController(IDownloadService downloadService)
        {
            this.downloadService = downloadService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await downloadService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var model = await downloadService.GetOneAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
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
