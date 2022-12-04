using BlenderParadise.Models;

namespace BlenderParadise.Services.Contracts
{
    public interface ISearchService
    {
        public Task<List<ViewProductModel>> SearchProductAsync(string modelName);
    }
}
