using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Models
{
    public class DownloadProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int Polygons { get; set; }

        [Required]
        public int Vertices { get; set; }

        [Required]
        public int Geometry { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string CoverPhoto { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public List<string> Photos { get; set; } = null!;

        [Required]
        public IFormFileCollection AttachmentModel { get; set; } = null!;
    }
}
