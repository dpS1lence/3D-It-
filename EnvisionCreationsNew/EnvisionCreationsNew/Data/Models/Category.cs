using System.ComponentModel.DataAnnotations;

namespace EnvisionCreationsNew.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(26)]
        public string Name { get; set; } = null!;
    }
}
