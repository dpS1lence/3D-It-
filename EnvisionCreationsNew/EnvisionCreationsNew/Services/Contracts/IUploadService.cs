using EnvisionCreationsNew.Models;

namespace EnvisionCreationsNew.Services.Contracts
{
    public interface IUploadService
    {
        Task UploadProductAsync(ProductModel model, string userId);
    }
}
