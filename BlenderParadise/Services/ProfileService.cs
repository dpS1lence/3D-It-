using BlenderParadise.Data;
using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlenderParadise.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await _repository.GetByIdAsync<ApplicationUser>(userId);

            return user;
        }
        public async Task<UserProfileModel> GetUserData(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user == null)
            {
                return null;
            }

            var usersProducts = _repository.All<ApplicationUserProduct>().Where(a => a.ApplicationUserId == user.Id);

            var userProducts = new List<UserProductModel>();

            foreach (var userProduct in usersProducts)
            {
                var product = await _repository.GetByIdAsync<Product>(userProduct.ProductId);

                var category = await _repository.GetByIdAsync<Category>(product.CategoryId);

                var photoStr = Convert.ToBase64String(product.Photo);

                var imageString = string.Format("data:image/jpg;base64,{0}", photoStr);

                userProducts.Add(new UserProductModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CoverPhoto = imageString,
                    Category = category.Name
                });
            }

            var model = new UserProfileModel()
            {
                Id = user?.Id,
                UserName = user?.UserName,
                Bio = user?.Description,
                ProfilePhoto = user?.ProfilePicture,
                UserModels = userProducts
            };

            return model;
        }

        public async Task<UserProfileModel> RemoveUserUploadAsync(string userId, int productId)
        {
            var user = await _userManager.Users
                .Include(a => a.ProductsData)
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var userProduct = user.ProductsData.FirstOrDefault(p => p.ProductId == productId);

            var product = await _repository.GetByIdAsync<Product>(userProduct.ProductId);

            var productContent = await _repository.All<ProductContent>().Where(a => a.ProductId == product.Id).FirstOrDefaultAsync();

            var content = await _repository.GetByIdAsync<Content>(productContent.ContentId);

            var productPhotos = _repository.All<ProductPhoto>().Where(p => p.ProductId == userProduct.ProductId);

            foreach (var productPhoto in productPhotos)
            {
                var photo = await _repository.GetByIdAsync<Photo>(productPhoto.PhotoId);

                _repository.Delete(productPhoto);
                _repository.Delete(photo);
            }

            _repository.Delete(productContent);
            _repository.Delete(content);
            _repository.Delete(product);

            if (userProduct != null)
            {
                user.ProductsData.Remove(userProduct);

                await _repository.SaveChangesAsync();
            }

            var model = await GetUserData(user.UserName);

            return model;
        }

        public async Task<EditProductModel> EditUserUploadAsync(int productId)
        {
            var desiredProduct = await _repository.GetByIdAsync<Product>(productId);

            if (desiredProduct == null)
            {
                return null;
            }

            var model = new EditProductModel()
            {
                Id = desiredProduct.Id,
                Name = desiredProduct.Name,
                Description = desiredProduct.Description
            };

            return model;
        }

        public async Task<bool> EditUserUploadAsync(EditProductModel model)
        {
            var desiredProduct = await _repository.GetByIdAsync<Product>(model.Id);

            if (desiredProduct == null)
            {
                return false;
            }

            desiredProduct.Name = model.Name;
            desiredProduct.Description = model.Description;

            _repository.Update(desiredProduct);

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
