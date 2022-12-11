using BlenderParadise.Data;
using BlenderParadise.Data.Models;
using BlenderParadise.Models.Product;
using BlenderParadise.Models.Profile;
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
            var user = await _userManager.Users
                .Include(a => a.ProductsData)
                .Where(a => a.UserName == userName)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid username.");
            }

            var userProductsData = user.ProductsData.ToList();

            var userProducts = new List<UserProductModel>();

            foreach (var userProduct in userProductsData)
            {
                var product = await _repository.GetByIdAsync<Product>(userProduct.Id);

                var category = await _repository.GetByIdAsync<Category>(product.CategoryId);

                if (category == null)
                {
                    throw new ArgumentException("Invalid request.");
                }

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
                throw new ArgumentException("Invalid request.");
            }

            var userProduct = user.ProductsData.FirstOrDefault(p => p.Id == productId);

            if (userProduct == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            var product = await _repository.GetByIdAsync<Product>(userProduct.Id);

            if (product == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            var content = await _repository.GetByIdAsync<Content>(product.ContentId);

            if (content == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            var photos = await _repository.All<Photo>().Where(p => p.ProductId == product.Id).ToListAsync();

            if (!photos.Any())
            {
                throw new ArgumentException("Invalid request.");
            }

            try
            {
                if (!_fileSaverService.DeleteFile(content.FileName))
                {
                    throw new ArgumentException("Invalid request.");
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid request.");
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
                throw new ArgumentException("Invalid request.");
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

        public async Task<EditProfileModel> EditUserProfileAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            var model = new EditProfileModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Description = user.Description,
                ProfilePicture = user.ProfilePicture
            };

            return model;
        }

        public async Task<bool> EditUserProfileAsync(EditProfileModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return false;
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Description = model.Description;
            user.ProfilePicture = model.ProfilePicture;

            _repository.Update(user);

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
