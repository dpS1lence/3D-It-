namespace EnvisionCreationsNew.Models
{
    public class UserProfileModel
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? Bio { get; set; }

        public List<UserProductModel> UserModels { get; set; } = null!;
    }
}
