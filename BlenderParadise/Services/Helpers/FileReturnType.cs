using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Services.Helpers
{
    public class FileReturnType
    {
        public FileReturnType(byte[] file, string type)
        {
            File = file;
            Type = type;
        }

        public byte[] File { get; private set; } = null!;
        public string Type { get; private set; } = null!;
    }
}
