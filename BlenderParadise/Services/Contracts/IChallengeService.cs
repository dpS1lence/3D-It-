namespace BlenderParadise.Services.Contracts
{
    public interface IChallengeService
    {
        Task GenerateChallengeAsync(string url, string apiKey);
        Task<string> GetChallengeAsync();
    }
}
