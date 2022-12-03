using BlenderParadise.Data;
using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.IO.Compression;

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
        public async Task<List<ViewProductModel>> GetAllAsync()
        {
            var entities = await _repository.All<Product>()
                .ToListAsync();

            var products = new List<ViewProductModel>();

            foreach (var item in entities)
            {
                var desiredCategory = await _repository.GetByIdAsync<Category>(item.CategoryId);

                var base64 = Convert.ToBase64String(item.Photo);

                var imgSrc = string.Format("data:image/jpg;base64,{0}", base64);
                products.Add(new ViewProductModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Category = desiredCategory?.Name ?? "-1",
                    Photo = imgSrc
                });
            }

            return products;
        }

        public async Task<DownloadProductModel> GetOneAsync(int id)
        {
            var productEntity = await _repository.GetByIdAsync<Product>(id);

            var desiredCategory = await _repository.GetByIdAsync<Category>(productEntity.CategoryId);

            var productPhotos = await _repository.All<ProductPhoto>().Where(a => a.ProductId == productEntity.Id).ToListAsync();

            var applicationUserProduct = await _repository.All<ApplicationUserProduct>()
                .Where(a => a.ProductId == productEntity.Id)
                .FirstOrDefaultAsync();

            var user = await _userManager.FindByIdAsync(applicationUserProduct.ApplicationUserId);

            var convertedPhoto = Convert.ToBase64String(productEntity.Photo);

            var imgSrc = string.Format("data:image/jpg;base64,{0}", convertedPhoto);

            var photos = new List<string>();

            foreach (var item in productPhotos)
            {
                var photo = await _repository.GetByIdAsync<Photo>(item.PhotoId);

                var photoStr = Convert.ToBase64String(photo.PhotoFile);

                var imageString = string.Format("data:image/jpg;base64,{0}", photoStr);

                photos.Add(imageString);
            }

            var product = new DownloadProductModel()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Polygons = productEntity.Polygons,
                Vertices = productEntity.Vertices,
                Geometry = productEntity.Geometry,
                UserId = user.Id,
                UserName = user.UserName,
                Category = desiredCategory?.Name ?? "-1",
                CoverPhoto = imgSrc,
                Photos = photos
            };

            return product;
        }

        public async Task<IActionResult> DownloadModelAsync(int id)
        {
            var entity = await _repository.All<ProductContent>()
                .Where(a => a.ProductId == id)
                .FirstOrDefaultAsync();

            var productEntity = await _repository.GetByIdAsync<Product>(id);

            try
            {
                //var contentEntity = await context.Content.FirstOrDefaultAsync(a => a.Id == entity.ContentId);
                var contentEntity = await _repository.GetByIdAsync<Content>(entity.ContentId);

                if (contentEntity == null)
                {
                    return null;
                }
                if (contentEntity?.AttachmentModel == null)
                {
                    return null;
                }

                byte[] byteArr = contentEntity?.AttachmentModel ?? null!;
                string mimeType = "application/blend";
                return new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = $"3DIt!_{productEntity?.Name.Replace(" ", "_")}.blend"
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> DownloadZipAsync(int id)
        {
            var entity = await _repository.All<ProductContent>()
                .Where(a => a.ProductId == id)
                .FirstOrDefaultAsync();

            var productEntity = await _repository.GetByIdAsync<Product>(id);

            try
            {
                //var contentEntity = await context.Content.FirstOrDefaultAsync(a => a.Id == entity.ContentId);
                var contentEntity = await _repository.GetByIdAsync<Content>(entity.ContentId);

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
