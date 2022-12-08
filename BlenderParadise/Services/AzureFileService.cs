using Azure.Storage.Blobs;
using BlenderParadise.Services.Contracts;

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

        public bool DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GetPath(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(IFormFile fileData)
        {
            string uniqueFileName = Guid.NewGuid() + "_" + fileData.FileName;

            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(uniqueFileName);

            var ms = new MemoryStream();
            fileData.CopyTo(ms);

            await blob.UploadAsync(ms);

            return uniqueFileName;
        }
    }
}
