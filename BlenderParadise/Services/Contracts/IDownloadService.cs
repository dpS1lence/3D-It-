using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Services.Contracts
{
    public interface IDownloadService
    {
        Task<string> DownloadModelAsync(int id);

        Task<IActionResult> DownloadZipAsync(int id);
    }
}
