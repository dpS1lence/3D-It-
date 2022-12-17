using BlenderParadise.Data;
using BlenderParadise.Data.Models;
using BlenderParadise.Models.Product;
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
        private readonly IFileService _fileSaverService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UploadService(IRepository repository, IFileService fileSaverService, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _fileSaverService = fileSaverService;
            _userManager = userManager;
        }

        public async Task<bool> UploadProductAsync(ProductModel model, string userId)
        {
            bool error = false;

            if (model == null || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(model.Category))
            {
                return error;
            }
            var desiredCategory = await _repository.All<Category>().Where(a => a.Name == model.Category).FirstOrDefaultAsync();

            var desiredUser = await _userManager.FindByIdAsync(userId);

            if (desiredCategory == null || desiredUser == null)
            {
                return error;
            }

            var contentEntity = new Content();
            string fileName = " ";
            try
            {
                fileName = await _fileSaverService.SaveFile(model.AttachmentModel[0]);
            }
            catch (Exception)
            {
                return error;
            }

            using (var target = new MemoryStream())
            {
                if (!model.PhotosZip.Any())
                {
                    return error;
                }

                model.PhotosZip[0].CopyTo(target);

                var photosCollection = target.ToArray();

                contentEntity = new Content()
                {
                    FileName = fileName,
                    PhotosZip = photosCollection
                };
            }

            try
            {
                await _repository.AddAsync(contentEntity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return error;
            }

            var productEntity = new Product();

            using (var target = new MemoryStream())
            {
                if (!model.CoverPhoto.Any())
                {
                    return error;
                }

                model.CoverPhoto[0].CopyTo(target);

                var coverPhoto = target.ToArray();

                var polygons = int.Parse(model.Polygons);
                if(polygons < 0) polygons = 0;

                var vertices = int.Parse(model.Vertices);
                if (vertices < 0) vertices = 0;

                var geometry = int.Parse(model.Geometry);
                if (geometry < 0) geometry = 0;

                productEntity = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Polygons = polygons,
                    Vertices = vertices,
                    Geometry = geometry,
                    CategoryId = desiredCategory?.Id ?? 1,
                    Photo = coverPhoto,
                    UserId = userId,
                    ApplicationUser = desiredUser,
                    ContentId = contentEntity.Id,
                    Content = contentEntity
                };
            }

            try
            {
                await _repository.AddAsync(productEntity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return error;
            }

            var photo = new Photo();

            foreach (var item in model.Photos)
            {
                var stream = new MemoryStream();

                item.CopyTo(stream);

                var photoResult = stream.ToArray();

                photo = new Photo()
                {
                    PhotoFile = photoResult,
                    ProductId = productEntity.Id,
                    Product = productEntity
                };

                try
                {
                    await _repository.AddAsync(photo);
                    await _repository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return error;
                }
            }

            return true;
        }
    }
}
