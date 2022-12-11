using BlenderParadise.Models.Product;

namespace BlenderParadise.Services.Contracts
{
    public interface ISearchService
    {
        public Task<List<ViewProductModel>> SearchProductAsync(string modelName);
    }
}
