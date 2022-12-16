using BlenderParadise.Models;

namespace BlenderParadise.Services.Contracts
{
    public interface IChallengeService
    {
        Task GenerateChallengeAsync(string url, string apiKey);
        Task<IndexModel> GetChallengeAsync();
    }
}
