using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace EnvisionCreationsNew.Models
{
    public class ViewProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Photo { get; set; } = null!;
    }
}
