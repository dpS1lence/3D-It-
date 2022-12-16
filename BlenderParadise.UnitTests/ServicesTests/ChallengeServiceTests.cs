using BlenderParadise.Models.Profile;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderParadise.UnitTests.ServicesTests
{
    public class ChallengeServiceTests : TestsBase
    {
        [Test]
        public async Task GetChallengeAsync_Should_Get_ChallengeAsync()
        {
            // Set up mock repository
            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Challange>()).Returns(challenges.BuildMock());

            // Create file service and profile service
            IChallengeService service = new ChallengeService(repoMock.Object);

            // Call the method being tested and store the result
            var actual = await service.GetChallengeAsync();

            // Verify that the mock repository was called as expected
            repoMock.VerifyAll();

            // Assert that the result is not null and has the expected type
            Assert.That(actual, Is.Not.Null);
            Assert.IsAssignableFrom<string>(actual);
        }
    }
}
