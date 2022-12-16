using Azure.Storage.Blobs;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Services
{

    public class AzureFileService : IFileService
    {
        private readonly string connectionString;
        private const string containerName = "fileupload";

        public AzureFileService(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);

            return await blob.DeleteIfExistsAsync();
        }

        public async Task<IActionResult> GetFile(string fileName)
        {
            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);

            byte[] buffer = Array.Empty<byte>();
            using (MemoryStream ms = new())
            {
                var response = await blob.DownloadToAsync(ms);

                if (response.IsError)
                {
                    throw new ArgumentException(response.Status.ToString());
                }

                buffer = ms.ToArray();
            }

            return new FileContentResult(buffer, "application/blend")
            {
                FileDownloadName = fileName
            };
        }

        public async Task<string> SaveFile(IFormFile fileData)
        {
            string uniqueFileName = Guid.NewGuid() + "_" + fileData.FileName;

            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(uniqueFileName);

            var ms = new MemoryStream();
            fileData.CopyTo(ms);

            ms.Position = 0;

            await blob.UploadAsync(ms);

            return uniqueFileName;
        }
    }
}
