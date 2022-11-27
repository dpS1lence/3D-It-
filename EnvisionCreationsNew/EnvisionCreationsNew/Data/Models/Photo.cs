using System.ComponentModel.DataAnnotations;

namespace EnvisionCreationsNew.Data.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] PhotoFile { get; set; } = null!;
    }
}
