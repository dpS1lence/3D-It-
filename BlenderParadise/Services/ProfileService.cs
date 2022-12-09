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
        private readonly IFileService _fileSaverService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileService(IRepository repository, IFileService fileSaverService, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _fileSaverService = fileSaverService;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }
        public async Task<UserProfileModel> GetUserData(string userName)
        {
            var user = new ApplicationUser();
            try
            {


                user = await _userManager.Users
                    .Include(a => a.ProductsData)
                    .Where(a => a.UserName == userName)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

            }
            if (user == null)
            {
                return null;
            }

            var userProductsData = user.ProductsData.ToList();

            var userProducts = new List<UserProductModel>();

            foreach (var userProduct in userProductsData)
            {
                var product = await _repository.GetByIdAsync<Product>(userProduct.Id);

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
            var user = _userManager.Users
                .Include(a => a.ProductsData)
                .Where(a => a.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var userProduct = user.ProductsData.FirstOrDefault(p => p.Id == productId);

            if (userProduct == null)
            {
                return null;
            }

            var product = await _repository.GetByIdAsync<Product>(userProduct.Id);

            if (product == null)
            {
                return null;
            }

            var content = await _repository.GetByIdAsync<Content>(product.ContentId);

            if (content == null)
            {
                return null;
            }

            var photos = _repository.All<Photo>().Where(p => p.ProductId == product.Id);

            if (photos == null)
            {
                return null;
            }

            try
            {
                if (!_fileSaverService.DeleteFile(content.FileName))
                {
                    return null;
                }
            }
            catch (IOException ioExp)
            {
                return null;
            }

            foreach (var photo in photos)
            {
                _repository.Delete(photo);
            }

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
