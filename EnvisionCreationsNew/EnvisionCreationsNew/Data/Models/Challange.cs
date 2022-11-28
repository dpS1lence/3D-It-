using System.ComponentModel.DataAnnotations;

namespace EnvisionCreationsNew.Data.Models
{
    public class Challange
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Link { get; set; } = null!;
    }
}
