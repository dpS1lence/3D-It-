using BlenderParadise.Data.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<string> GetNameAsync(int id)
        {
            var productEntity = await _repository.GetByIdAsync<Product>(id);

            var contentEntity = await _repository.GetByIdAsync<Content>(productEntity.ContentId);

            if (contentEntity == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            return contentEntity.FileName;
        }

        public async Task<IActionResult> GetZipAsync(int id)
        {
            var productEntity = await _repository.GetByIdAsync<Product>(id);

            var contentEntity = await _repository.GetByIdAsync<Content>(productEntity.ContentId);

            if (contentEntity == null || contentEntity.PhotosZip == null || contentEntity.PhotosZip.Equals(Array.Empty<byte>()))
            {
                throw new ArgumentException("Invalid request.");
            }

            byte[] byteArr = contentEntity.PhotosZip;
            string mimeType = "application/rar";

            return new FileContentResult(byteArr, mimeType)
            {
                FileDownloadName = $"{contentEntity.FileName.Replace(" ", "_")}_textures.rar"
            };
        }
    }
}
