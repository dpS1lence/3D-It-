using BlenderParadise.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Services.Contracts
{
    public interface IFileService
    {
        Task<IActionResult> GetFile(string fileName);
        Task<string> SaveFile(IFormFile fileData);
        Task<bool> DeleteFile(string fileName);
    }
}
