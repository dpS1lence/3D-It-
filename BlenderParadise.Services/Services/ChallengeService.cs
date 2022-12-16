using BlenderParadise.Data.Models;
using BlenderParadise.Models;
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

            var all = repo.All<Challange>().ToList();

            if(all.Count > 2)
            {
                var prevChallenge = all.TakeLast(2).ToList().First();
                repo.Delete(prevChallenge);
            }

            await repo.AddAsync(challenge);
            await repo.SaveChangesAsync();
        }

        public async Task<IndexModel> GetChallengeAsync()
        {
            var challengeLast = (await repo.All<Challange>().ToListAsync()).Last();
            var challengePrev = (await repo.All<Challange>().ToListAsync()).TakeLast(2).First();

            if(challengeLast == null || challengePrev == null)
            {
                throw new ArgumentException("Invalid request.");
            }

            return new IndexModel()
            {
                Challenge = challengeLast.Name,
                PrevChallenge = challengePrev.Name,
                Link = challengeLast.Link
            };
        }
    }
}
