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
            if (model == null || userId == null)
            {
                return false;
            }
            var desiredCategory = await _repository.All<Category>().Where(a => a.Name == model.Category).FirstOrDefaultAsync();

            var desiredUser = await _userManager.FindByIdAsync(userId);

            if (desiredCategory == null || desiredUser == null)
            {
                return false;
            }

            var contentEntity = new Content();

            string fileName = await _fileSaverService.SaveFile(model.AttachmentModel[0]);

            using (var target = new MemoryStream())
            {
                model.PhotosZip[0].CopyTo(target);

                var photosCollection = target.ToArray();

                contentEntity = new Content()
                {
                    FileName = fileName,
                    PhotosZip = photosCollection
                };
            }

            await _repository.AddAsync(contentEntity);

            try
            {
                await _repository.SaveChangesAsync();

            }
            catch (Exception)
            {
                return false;
            }

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
                    Photo = coverPhoto,
                    UserId = userId,
                    ApplicationUser = desiredUser,
                    ContentId = contentEntity.Id,
                    Content = contentEntity
                };
            }

            await _repository.AddAsync(productEntity);

            try
            {
                await _repository.SaveChangesAsync();

            }
            catch (Exception)
            {
                return false;
            }

            var photo = new Photo();

            var stream = new MemoryStream();

            foreach (var item in model.Photos)
            {
                stream = new MemoryStream();

                item.CopyTo(stream);

                var photoResult = stream.ToArray();

                photo = new Photo()
                {
                    PhotoFile = photoResult,
                    ProductId = productEntity.Id,
                    Product = productEntity
                };

                await _repository.AddAsync(photo);

                try
                {
                    await _repository.SaveChangesAsync();

                }
                catch (Exception)
                {
                    return false;
                }
            }



            return true;
        }
    }
}
