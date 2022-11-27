using System.ComponentModel.DataAnnotations;

namespace EnvisionCreationsNew.Models
{
    public class ProductModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Polygons { get; set; } = null!;

        public string Vertices { get; set; } = null!;

        public string Geometry { get; set; } = null!;

        public string Category { get; set; } = null!;

        public IFormFileCollection AttachmentModel { get; set; } = null!;

        public IFormFileCollection CoverPhoto { get; set; } = null!;

        public IFormFileCollection Photos { get; set; } = null!;

        public IFormFileCollection PhotosZip { get; set; } = null!;
    }
}
