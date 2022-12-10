using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BlenderParadise.Models
{
    public class ViewProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string Photo { get; set; } = null!;
    }
}
