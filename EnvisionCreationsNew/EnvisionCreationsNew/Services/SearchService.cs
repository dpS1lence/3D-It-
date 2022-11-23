using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Models;
using EnvisionCreationsNew.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EnvisionCreationsNew.Services
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext context;
        public SearchService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<List<ViewProductModel>> SearchProductAsync(string modelName)
        {
            var matchingProducts = await context.Products.Where(p => p.Name == modelName).ToListAsync();

            var products = new List<ViewProductModel>();

            foreach (var item in matchingProducts)
            {
                var desiredCategory = await context.Categories.FirstOrDefaultAsync(a => a.Id == item.CategoryId);

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
