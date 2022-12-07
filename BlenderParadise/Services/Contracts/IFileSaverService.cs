namespace BlenderParadise.Services.Contracts
{
    public interface IFileSaverService
    {
        string GetPath(string fileName);
        string SaveFile(IFormFile fileData);
    }
}
