using BlenderParadise.Data.Models;

namespace BlenderParadise.Tests.Common
{
    public class BlenderParadiseTestDb
    {
        public BlenderParadiseTestDb()
        {

        }

        public ApplicationUser User { get; set; }
        public ApplicationUser UserWithUpload { get; set; }
        public ApplicationUser UserWithMostUploads { get; set; }
        //public ApplicationUser B { get; set; }
    }
}