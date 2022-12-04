using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Services.Contracts
{
    public interface IDownloadService
    {
        Task<List<ViewProductModel>> GetAllAsync();

        Task<DownloadProductModel> GetOneAsync(int id);

        Task<IActionResult> DownloadModelAsync(int id);

        Task<IActionResult> DownloadZipAsync(int id);
    }
}
