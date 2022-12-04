using BlenderParadise.Models;

namespace BlenderParadise.Services.Contracts
{
    public interface IUploadService
    {
        Task<bool> UploadProductAsync(ProductModel model, string userId);
    }
}
