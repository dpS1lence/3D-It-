﻿using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

            var productEntity = new Product();

            using (var target = new MemoryStream())
            {
                model.Photo[0].CopyTo(target);

                var photo = target.ToArray();
                try
                {
                    Product p = new Product();
                    p.Name = model.Name;
                    p.Description = model.Description;
                    p.Polygons = int.Parse(model.Polygons);
                    p.Vertices = int.Parse(model.Vertices);
                    p.Geometry = int.Parse(model.Geometry);
                    p.CategoryId = desiredCategory?.Id ?? 1;
                    p.Photo = photo;
                    productEntity = new Product()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Polygons = int.Parse(model.Polygons),
                        Vertices = int.Parse(model.Vertices),
                        Geometry = int.Parse(model.Geometry),
                        CategoryId = desiredCategory?.Id ?? 1,
                        Photo = photo
                    };
                }
                catch(Exception){

                }
            }


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

            var desiredUser = await context.Users.FirstOrDefaultAsync(a => a.Id == userId);

            var applicationUserProductEntity = new ApplicationUserProduct()
            {
                ApplicationUser = desiredUser,
                ApplicationUserId = desiredUser?.Id ?? "-1",
                Product = productEntity,
                ProductId = productEntity.Id
            };

            var productContentEntity = new ProductContent()
            {
                Content = contentEntity,
                ContentId = contentEntity.Id,
                Product = productEntity,
                ProductId = productEntity.Id
            };

            await context.ProductsContent.AddAsync(productContentEntity);
            await context.ApplicationUsersProducts.AddAsync(applicationUserProductEntity);

            await context.SaveChangesAsync();
        }
    }
}
