namespace EnvisionCreationsNew.Models
{
    public class DownloadProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Polygons { get; set; }

        public int Vertices { get; set; }

        public int Geometry { get; set; }

        public string Category { get; set; } = null!;

        public string CoverPhoto { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public List<string> Photos { get; set; } = null!;

        public IFormFileCollection AttachmentModel { get; set; } = null!;
    }
}
