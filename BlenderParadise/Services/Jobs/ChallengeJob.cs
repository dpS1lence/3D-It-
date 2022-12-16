using BlenderParadise.Services.Contracts;
using Quartz;

namespace BlenderParadise.Services.Jobs
{
    public class ChallengeJob : IJob
    {
        private readonly IChallengeService challengeService;
        private readonly IConfiguration config;

        public ChallengeJob(IChallengeService challengeService, IConfiguration config)
        {
            this.challengeService = challengeService;
            this.config = config;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await challengeService.GenerateChallengeAsync(config.GetValue<string>("ApiNinjas"), config.GetValue<string>("Key"));
        }
    }
}
