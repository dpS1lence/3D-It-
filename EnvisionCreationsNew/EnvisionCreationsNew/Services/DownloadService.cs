using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.IO.Compression;

namespace EnvisionCreationsNew.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly ApplicationDbContext context;
        public DownloadService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<List<ViewProductModel>> GetAllAsync()
        {
            var entities = await context.Products
                .ToListAsync();

            var products = new List<ViewProductModel>();

            foreach (var item in entities)
            {
                var desiredCategory = await context.Categories.FirstOrDefaultAsync(a => a.Id == item.CategoryId);

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

        public async Task<DownloadProductModel> GetOneAsync(int? id)
        {
            var productEntity = await context.Products.FirstOrDefaultAsync(a => a.Id == id);

            var desiredCategory = await context.Categories.FirstOrDefaultAsync(a => a.Id == productEntity.CategoryId);

            var productPhotos = await context.ProductPhotos.Where(a => a.ProductId == productEntity.Id).ToListAsync();

            var applicationUserProduct = await context.ApplicationUsersProducts.FirstOrDefaultAsync(a => a.ProductId == productEntity.Id);

            var user = await context.Users.FirstOrDefaultAsync(a => a.Id == applicationUserProduct.ApplicationUserId);

            var convertedPhoto = Convert.ToBase64String(productEntity.Photo);

            var imgSrc = string.Format("data:image/jpg;base64,{0}", convertedPhoto);

            var photos = new List<string>();

            foreach (var item in productPhotos)
            {
                var photo = await context.Photos.FirstOrDefaultAsync(a => a.Id == item.PhotoId);

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

        public async Task<IActionResult> DownloadModelAsync(int? id)
        {
            var entity = await context.ProductsContent.FirstOrDefaultAsync(a => a.ProductId == id);

            var productEntity = await context.Products.FirstOrDefaultAsync(a => a.Id == id);

            try
            {
                //var contentEntity = await context.Content.FirstOrDefaultAsync(a => a.Id == entity.ContentId);
                var contentEntity = context.Content.FirstOrDefault(a => a.Id == entity.ContentId);

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

        public async Task<IActionResult> DownloadZipAsync(int? id)
        {
            var entity = await context.ProductsContent.FirstOrDefaultAsync(a => a.ProductId == id);

            var productEntity = await context.Products.FirstOrDefaultAsync(a => a.Id == id);

            try
            {
                //var contentEntity = await context.Content.FirstOrDefaultAsync(a => a.Id == entity.ContentId);
                var contentEntity = context.Content.FirstOrDefault(a => a.Id == entity.ContentId);

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
