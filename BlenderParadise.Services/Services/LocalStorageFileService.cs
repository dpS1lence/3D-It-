using BlenderParadise.Data.Models;
using BlenderParadise.Services.Contracts;
using BlenderParadise.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;

namespace BlenderParadise.Services
{
    public class LocalStorageFileService : IFileService
    {
        private readonly string webRootPath;

        public LocalStorageFileService(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }
        public async Task<IActionResult> GetFile(string fileName)
        {
            string filePath = Path.Combine(Path.Combine(webRootPath, "databaseFiles"), fileName);

            byte[] bytes = await File.ReadAllBytesAsync(filePath);

            return new FileContentResult(bytes, "application/blend")
            {
                FileDownloadName = fileName
            };
        }
        public async Task<string> SaveFile(IFormFile fileData)
        {
            string uniqueFileName = String.Empty;

            if (fileData != null)
            {
                var uploadsFolder = Path.Combine(webRootPath, "databaseFiles");
                uniqueFileName = Guid.NewGuid() + "_" + fileData.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await fileData.CopyToAsync(fileStream);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Invalid file data.");
                }
            }

            return uniqueFileName;
        }
        public async Task<bool> DeleteFile(string fileName)
        {
            string filePath = Path.Combine(Path.Combine(webRootPath, "databaseFiles"), fileName);
            // Check if file exists with its full path    
            if (File.Exists(filePath))
            {
                await File.ReadAllLinesAsync(filePath);

                // If file found, delete it    
                File.Delete(filePath);
            }

            else return false;

            return true;
        }
    }
}
