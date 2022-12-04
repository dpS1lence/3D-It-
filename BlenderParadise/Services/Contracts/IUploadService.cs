using BlenderParadise.Models;

namespace BlenderParadise.Services.Contracts
{
    public interface IUploadService
    {
        Task UploadProductAsync(ProductModel model, string userId);
    }
}
