using BlenderParadise.Data;
using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlenderParadise.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public DownloadService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<string> DownloadModelAsync(int id)
        {

            var productEntity = await _repository.GetByIdAsync<Product>(id);

            try
            {
                var contentEntity = _repository.GetDownloadById(productEntity.ContentId);

                if (contentEntity == null)
                {
                    return null;
                }

                return contentEntity.FileName;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IActionResult> DownloadZipAsync(int id)
        {
            var productEntity = await _repository.GetByIdAsync<Product>(id);

            try
            {
                var contentEntity = _repository.GetDownloadById(productEntity.ContentId);

                if (contentEntity == null)
                {
                    return null;
                }
                if (contentEntity?.PhotosZip == null)
                {
                    return null;
                }

                byte[] byteArr = contentEntity?.PhotosZip ?? null!;
                string mimeType = "application/rar";

                return new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = $"3DIt!_{productEntity?.Name.Replace(" ", "_")}_textures.rar"
                };

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
