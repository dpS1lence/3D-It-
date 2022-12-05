using BlenderParadise.Models;

namespace BlenderParadise.Services.Contracts
{
    public interface IProductService
    {
        Task<List<ViewProductModel>> GetAllAsync();

        Task<DownloadProductModel> GetOneAsync(int id);
    }
}
