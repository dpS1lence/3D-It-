using BlenderParadise.Data;
using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BlenderParadise.Services
{
    public class UploadService : IUploadService
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UploadService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task UploadProductAsync(ProductModel model, string userId)
        {
            var desiredCategory = await _repository.All<Category>().Where(a => a.Name == model.Category).FirstOrDefaultAsync();

            var productEntity = new Product();

            using (var target = new MemoryStream())
            {
                model.CoverPhoto[0].CopyTo(target);

                var coverPhoto = target.ToArray();

                productEntity = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Polygons = int.Parse(model.Polygons),
                    Vertices = int.Parse(model.Vertices),
                    Geometry = int.Parse(model.Geometry),
                    CategoryId = desiredCategory?.Id ?? 1,
                    Photo = coverPhoto
                };
            }

            var contentEntity = new Content();

            using (var target = new MemoryStream())
            {
                model.AttachmentModel[0].CopyTo(target);

                var attachmentModel = target.ToArray();

                model.PhotosZip[0].CopyTo(target);

                var photosCollection = target.ToArray();

                contentEntity = new Content()
                {
                    AttachmentModel = attachmentModel,
                    PhotosZip = photosCollection
                };
            }
            
            
            await _repository.AddAsync(contentEntity);
            await _repository.AddAsync(productEntity);

            try
            {
                await _repository.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }

            var photo = new Photo();
            var productPhoto = new ProductPhoto();

            var stream = new MemoryStream();

            foreach (var item in model.Photos)
            {
                stream = new MemoryStream();

                item.CopyTo(stream);

                var photoResult = stream.ToArray();

                photo = new Photo()
                {
                    PhotoFile = photoResult
                };

                await _repository.AddAsync(photo);

                try
                {
                    await _repository.SaveChangesAsync();

                }
                catch (Exception ex)
                {

                }

                productPhoto = new ProductPhoto()
                {
                    Photo = photo,
                    PhotoId = photo.Id,
                    Product = productEntity,
                    ProductId = productEntity.Id
                };

                await _repository.AddAsync(productPhoto);
            }

            var desiredUser = await _userManager.FindByIdAsync(userId);

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

            await _repository.AddAsync(productContentEntity);
            await _repository.AddAsync(applicationUserProductEntity);

            await _repository.SaveChangesAsync();
        }
    }
}
