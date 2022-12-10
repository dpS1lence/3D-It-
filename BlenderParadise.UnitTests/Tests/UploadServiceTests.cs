using BlenderParadise.Data.Models;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;

namespace BlenderParadise.UnitTests.Tests
{
    [TestFixture]
    public class UploadServiceTests : TestsBase
    {
        [Test]
        public void GetUserData_Should_Get_User_Data()
        {
            var users = testDb.users.ToList();
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            var model = new ProductModel()
            {
                Name = "a",
                Description = "d",
                Polygons = "2",
                Vertices = "2",
                Geometry = "2",
                Category = "Category1",
                AttachmentModel = new FormFileCollection()
                {
                    new FormFile(new MemoryStream(), 1, 1, "a", "a")
                },
                Photos = new FormFileCollection()
                {
                    new FormFile(new MemoryStream(), 1, 1, "a", "a")
                },
                PhotosZip = new FormFileCollection()
                {
                    new FormFile(new MemoryStream(), 1, 1, "a", "a")
                },
                CoverPhoto = new FormFileCollection()
                {
                    new FormFile(new MemoryStream(), 1, 1, "a", "a")
                }
            };

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
            IUploadService service = new UploadService(repoMock.Object, fileService, this.userManager.Object);

            var actual = service.UploadProductAsync(model, testDb.User.Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.EqualTo(false));
        }
    }
}
