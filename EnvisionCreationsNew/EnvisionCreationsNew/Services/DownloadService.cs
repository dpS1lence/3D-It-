using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

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
            var entity = await context.Products.FirstOrDefaultAsync(a => a.Id == id);

            var desiredCategory = await context.Categories.FirstOrDefaultAsync(a => a.Id == entity.CategoryId);

            var base64 = Convert.ToBase64String(entity.Photo);

            var imgSrc = string.Format("data:image/jpg;base64,{0}", base64);

            var product = new DownloadProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Polygons = entity.Polygons,
                Vertices = entity.Vertices,
                Geometry = entity.Geometry,
                Category = desiredCategory?.Name ?? "-1",
                Photo = imgSrc
            };

            return product;
        }

        public async Task<IActionResult> DownloadAsync(int? id)
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
    }
}
