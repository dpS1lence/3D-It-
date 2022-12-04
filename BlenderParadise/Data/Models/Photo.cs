using System.ComponentModel.DataAnnotations;

namespace BlenderParadise.Data.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] PhotoFile { get; set; } = null!;
    }
}
