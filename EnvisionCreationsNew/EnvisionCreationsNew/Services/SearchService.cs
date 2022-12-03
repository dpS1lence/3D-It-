using BlenderParadise.Data;
using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlenderParadise.Services
{
    public class SearchService : ISearchService
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public SearchService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<List<ViewProductModel>> SearchProductAsync(string modelName)
        {
            var matchingProducts = await _repository.All<Product>().Where(p => p.Name.ToLower().Contains(modelName.ToLower())).ToListAsync();

            var products = new List<ViewProductModel>();

            foreach (var item in matchingProducts)
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
    }
}
