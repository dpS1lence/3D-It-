using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using BlenderParadise.Services;
using BlenderParadise.Tests.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderParadise.UnitTests.Tests
{
    public class ProfileServiceTests : TestsBase
    {
        private Mock<IRepository>? repoMock;
        private readonly BlenderParadiseTestDb testDb = new();

        [Test]
        public void ProfileService_GetUserData_Should_Get_User_Data()
        {
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));
            IFileService fileService = new LocalStorageFileService("");
            IProfileService service = new ProfileService(repoMock.Object, fileService, this.userManager.Object);

            var actual = service.GetUserData(testDb.User.UserName);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<UserProfileModel>(actual.Result);
            Assert.Multiple(() =>
            {
                Assert.That(actual.Result.UserName, Is.EqualTo(testDb.User.UserName));
                Assert.That(actual.Result.UserModels, Has.Count.EqualTo(testDb.User.ProductsData.Count));
            });
        }

        [Test]
        public void ProfileService_GetUserById_Should_Get_User_By_Id()
        {
/*            repoMock = new Mock<IRepository>();
            IFileService fileService = new LocalStorageFileService("");
            IProfileService service = new ProfileService(repoMock.Object, fileService, this.userManager.Object);

            var actual = service.GetUserById(testDb.User.Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.That(actual.Result.Id, Is.EqualTo(testDb.User.Id));*/
        }
    }
}
