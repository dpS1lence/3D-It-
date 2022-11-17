using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EnvisionCreationsNew.Controllers
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
        [HttpPost]
        public async Task<IActionResult> All(int? id)
        {
            var model = await downloadService.GetOneAsync(id);

            return RedirectToAction(nameof(Download), model);
        }
        [HttpGet]
        public IActionResult Download(DownloadProductModel model)
        {
            return View(model);
        }

        [HttpPost]
        [RequestSizeLimit(209715200)]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<IActionResult> Download(int? id)
        {
            var file = await downloadService.DownloadAsync(id);

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
