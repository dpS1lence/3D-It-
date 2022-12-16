using BlenderParadise.Data.Models;
using BlenderParadise.Models.Product;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace BlenderParadise.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IRepository repo;

        public ChallengeService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task GenerateChallengeAsync(string url, string apiKey)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            var json = await client.GetStringAsync(url);

            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("Invalid api responce.");
            }

            var result = JsonConvert.DeserializeObject<ChallengeModel>(json);

            if (result == null)
            {
                throw new ArgumentException("Invalid api responce.");
            }

            var challenge = new Challange()
            {
                Name = result.Hobby,
                Category = result.Category,
                Link = result.Link
            };

            await repo.AddAsync(challenge);
            await repo.SaveChangesAsync();
        }

        public async Task<string> GetChallengeAsync()
        {
            var challenge = (await repo.All<Challange>().ToListAsync()).Last();

            if(challenge == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            return challenge.Name;
        }
    }
}
