using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace BlenderParadise.Services
{
    public class LocalStorageFileSaverService : IFileSaverService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public LocalStorageFileSaverService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public string GetPath(string fileName)
        {
            return Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, "databaseFiles"), fileName);
        }
        public string SaveFile(IFormFile fileData)
        {
            string uniqueFileName = String.Empty;

            if (fileData != null)
            {
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "databaseFiles");
                uniqueFileName = Guid.NewGuid() + "_" + fileData.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    fileData.CopyTo(fileStream);
                }
                catch (Exception)
                {

                }
            }

            return uniqueFileName;
        }
    }
}
