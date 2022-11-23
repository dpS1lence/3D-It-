using EnvisionCreationsNew.Models;

namespace EnvisionCreationsNew.Services.Contracts
{
    public interface ISearchService
    {
        public Task<List<ViewProductModel>> SearchProductAsync(string modelName);
    }
}
