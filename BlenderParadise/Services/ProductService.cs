using BlenderParadise.Data.Models;
using BlenderParadise.Models.Product;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlenderParadise.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<List<ViewProductModel>> GetAllAsync()
        {
            var entities = _repository.All<Product>().ToList();

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

            if (productEntity == null)
            {
                throw new ArgumentException("Invalid product id.");
            }

            var desiredCategory = await _repository.GetByIdAsync<Category>(productEntity.CategoryId);

            if (desiredCategory == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            var productPhotos = _repository.All<Photo>().Where(a => a.ProductId == productEntity.Id).ToList();

            if (productPhotos == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            var user = await _userManager.FindByIdAsync(productEntity.UserId);

            if (user == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            var convertedPhoto = Convert.ToBase64String(productEntity.Photo);

            var imgSrc = string.Format("data:image/jpg;base64,{0}", convertedPhoto);

            var photos = new List<string>();

            foreach (var item in productPhotos)
            {
                var photoStr = Convert.ToBase64String(item.PhotoFile);

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
    }
}
