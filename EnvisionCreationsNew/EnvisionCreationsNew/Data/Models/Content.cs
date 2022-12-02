using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Data.Models
{
    public class Content
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] AttachmentModel { get; set; } = null!;

        [Required]
        public byte[] PhotosZip { get; set; } = null!;
    }
}
