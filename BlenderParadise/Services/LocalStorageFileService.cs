using BlenderParadise.Data.Models;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace BlenderParadise.Services
{
    public class LocalStorageFileService : IFileService
    {
        private readonly string webRootPath;

        public LocalStorageFileService(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }
        public string GetPath(string fileName)
        {
            return Path.Combine(Path.Combine(webRootPath, "databaseFiles"), fileName);
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
        public bool DeleteFile(string fileName)
        {
            string filePath = GetPath(fileName);
            // Check if file exists with its full path    
            if (File.Exists(filePath))
            {
                // If file found, delete it    
                File.Delete(filePath);
            }
            else return false;

            return true;
        }
    }
}
