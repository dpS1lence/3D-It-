using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Services.Contracts
{
    public interface IDownloadService
    {
        Task<string> GetNameAsync(int id);

        Task<IActionResult> GetZipAsync(int id);
    }
}
