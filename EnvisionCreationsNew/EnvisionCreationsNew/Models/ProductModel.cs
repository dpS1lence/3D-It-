using System.ComponentModel.DataAnnotations;

namespace EnvisionCreationsNew.Models
{
    public class ProductModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Polygons { get; set; }

        public int Vertices { get; set; }

        public int Geometry { get; set; }

        public string Category { get; set; } = null!;

        public IFormFileCollection AttachmentModel { get; set; } = null!;

        public IFormFileCollection Photo { get; set; } = null!;
    }
}
