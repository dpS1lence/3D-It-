using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EnvisionCreationsNew.Services
{
    public class UploadService : IUploadService
    {
        private readonly ApplicationDbContext context;
        public UploadService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task UploadProductAsync(ProductModel model, string userId)
        {
            var desiredCategory = await context.Categories.FirstOrDefaultAsync(a => a.Name == model.Category);

            var productEntity = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Polygons = model.Polygons,
                Vertices = model.Vertices,
                Geometry = model.Geometry,
                CategoryId = desiredCategory?.Id ?? 1
            };


            var contentEntity = new Content();

            using (var target = new MemoryStream())
            {
                model.AttachmentModel[0].CopyTo(target);

                var attachmentModel = target.ToArray();

                model.Photo[0].CopyTo(target);

                var photo = target.ToArray();

                contentEntity = new Content()
                {
                    AttachmentModel = attachmentModel,
                    Photo = photo
                };
            }

            await context.Products.AddAsync(productEntity);
            await context.Content.AddAsync(contentEntity);

            await context.SaveChangesAsync();

            productEntity = await context.Products.OrderBy(a => a.Id).LastOrDefaultAsync();
            contentEntity = await context.Content.OrderBy(a => a.Id).LastOrDefaultAsync();

            var desiredUser = await context.Users.FirstOrDefaultAsync(a => a.Id == userId);

            var applicationUserProductEntity = new ApplicationUserProduct()
            {
                ApplicationUser = desiredUser,
                ApplicationUserId = desiredUser?.Id ?? "-1",
                Product = productEntity,
                ProductId = productEntity?.Id ?? 1
            };

            var productContentEntity = new ProductContent()
            {
                Content = contentEntity,
                ContentId = contentEntity?.Id ?? 1,
                Product = productEntity,
                ProductId = productEntity?.Id ?? 1
            };

            await context.ProductsContent.AddAsync(productContentEntity);
            await context.ApplicationUsersProducts.AddAsync(applicationUserProductEntity);

            await context.SaveChangesAsync();
        }
    }
}
