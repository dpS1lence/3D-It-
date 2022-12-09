namespace BlenderParadise.UnitTests.Tests
{
    [TestFixture]
    public class ProfileServiceTests : TestsBase
    {
        [Test]
        public void ProfileService_GetUserData_Should_Get_User_Data()
        {
            var users = testDb.users.ToList();
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
            repoMock = new Mock<IRepository>();
            IFileService fileService = new LocalStorageFileService("");
            IProfileService service = new ProfileService(repoMock.Object, fileService, this.userManager.Object);

            var actual = service.GetUserById(testDb.User.Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.That(actual.Result.Id, Is.EqualTo(testDb.User.Id));
        }

        [Test]
        public void ProfileService_RemoveUserUploadAsync_Should_Remove_User_Upload()
        {
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            var content = testDb.content.ToList();

            var photos = testDb.photos.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.Delete(It.IsAny<Photo>()));
            repoMock.Setup(r => r.Delete(It.IsAny<Content>()));
            repoMock.Setup(r => r.Delete(It.IsAny<Product>()));
            repoMock.Setup(r => r.All<Photo>()).Returns(photos.AsQueryable());
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));
            IProfileService service = new ProfileService(repoMock.Object, this.fileService, this.userManager.Object);

            int beforeRemoveCount = testDb.User.ProductsData.Count;

            var actual = service.RemoveUserUploadAsync(testDb.User.Id, testDb.User.ProductsData.ToList().First().Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<UserProfileModel>(actual.Result);
            Assert.That(actual.Result.UserName, Is.EqualTo(testDb.User.UserName));
        }

        [Test]
        public void ProfileService_EditUserUploadAsync_Should_Return_View_Model()
        {
            var products = testDb.products.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));
            IProfileService service = new ProfileService(repoMock.Object, this.fileService, this.userManager.Object);

            int beforeRemoveCount = testDb.User.ProductsData.Count;

            var actual = service.EditUserUploadAsync(products.First().Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<EditProductModel>(actual.Result);
            Assert.That(actual.Result.Name, Is.EqualTo(testDb.products.First().Name));
        }

        [Test]
        public void ProfileService_EditUserUploadAsync_Should_Update_User_Upload()
        {
            var products = testDb.products.ToList();

            var model = new EditProductModel()
            {
                Id= 1,
                Name = "Name",
                Description = "Description"
            };

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));
            IProfileService service = new ProfileService(repoMock.Object, this.fileService, this.userManager.Object);

            int beforeRemoveCount = testDb.User.ProductsData.Count;

            var actual = service.EditUserUploadAsync(model);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.EqualTo(false));
            Assert.IsAssignableFrom<bool>(actual.Result);
        }
    }
}
