using BlenderParadise.Models.Product;

namespace BlenderParadise.Services.Contracts
{
    public interface IUploadService
    {
        Task<bool> UploadProductAsync(ProductModel model, string userId);
    }
}
