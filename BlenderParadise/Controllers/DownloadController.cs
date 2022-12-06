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
            var filePath = await downloadService.DownloadModelAsync(id);

            if (filePath == null)
            {
                return NotFound();
            }
            else
            {
                string mimeType = "application/blend";

                return File("F:\\3_DavidDocuments\\Work\\SOFT-UNI\\ASP.NET Project for exam\\GitHub\\3D-It-\\BlenderParadise\\wwwroot\\databaseFiles\\" + filePath, mimeType);
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
