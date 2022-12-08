namespace BlenderParadise.Services.Contracts
{
    public interface IFileService
    {
        string GetPath(string fileName);
        Task<string> SaveFile(IFormFile fileData);
        bool DeleteFile(string fileName);
    }
}
