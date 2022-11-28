using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EnvisionCreationsNew.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        public UserService(ApplicationDbContext _context)
        {
            context = _context;
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
    }
}
