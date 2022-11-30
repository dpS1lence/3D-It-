using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EnvisionCreationsNew.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        public UserService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(a => a.Id == userId);

            return user;
        }
        public async Task<UserProfileModel> GetUserData(string userName)
        {
            var user = await context.Users.FirstOrDefaultAsync(a => a.UserName == userName);

            var usersProducts = await context.ApplicationUsersProducts.Where(a => a.ApplicationUserId == user.Id).ToListAsync();

            var userProducts = new List<UserProductModel>();

            foreach (var userProduct in usersProducts)
            {
                var product = await context.Products.FirstOrDefaultAsync(a => a.Id == userProduct.ProductId);

                var category = await context.Categories.FirstOrDefaultAsync(a => a.Id == product.CategoryId);

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
                Id = user.Id,
                UserName = user?.UserName,
                Bio = user?.Email,
                UserModels = userProducts
            };

            return model;
        }

        public async Task<UserProfileModel> RemoveUserUploadAsync(string userId, int productId)
        {
            var user = await context.Users.Include(a => a.ProductsData).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var userProduct = user.ProductsData.FirstOrDefault(p => p.ProductId == productId);

            var product = await context.Products.FirstOrDefaultAsync(a => a.Id == userProduct.ProductId);

            var productContent = await context.ProductsContent.FirstOrDefaultAsync(c => c.ProductId == userProduct.ProductId);

            var content = await context.Content.FirstOrDefaultAsync(c => c.Id == productContent.ContentId);

            var productPhotos = context.ProductPhotos.Where(p => p.ProductId == userProduct.ProductId);

            foreach (var productPhoto in productPhotos)
            {
                var photo = await context.Photos.FirstOrDefaultAsync(p => p.Id == productPhoto.PhotoId);

                context.ProductPhotos.Remove(productPhoto);

                context.Photos.Remove(photo);
            }

            context.ProductsContent.Remove(productContent);

            context.Content.Remove(content);

            context.Products.Remove(product);

            if (userProduct != null)
            {
                user.ProductsData.Remove(userProduct);

                await context.SaveChangesAsync();
            }
            var model = await GetUserData(user.UserName);

            return model;
        }
    }
}
