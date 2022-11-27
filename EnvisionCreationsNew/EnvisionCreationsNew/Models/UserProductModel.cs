namespace EnvisionCreationsNew.Models
{
    public class UserProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string CoverPhoto { get; set; } = null!;
    }
}
