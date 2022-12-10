using BlenderParadise.Data.Models;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;

namespace BlenderParadise.UnitTests.ServicesTests
{
    [TestFixture]
    public class UploadServiceTests : TestsBase
    {
        [Test]
        public void GetUserData_Should_Get_User_Data()
        {
            var model = testDb.productModel;

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
            IUploadService service = new UploadService(repoMock.Object, fileService, userManager.Object);

            var actual = service.UploadProductAsync(model, testDb.User.Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.EqualTo(false));
        }

        [Test]
        //Test that the method returns false if the model parameter is null.
        public void GetUserData_Should_Return_False_If_The_Model_Is_Null()
        {
            var model = new ProductModel();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
            IUploadService service = new UploadService(repoMock.Object, fileService, userManager.Object);

            var actual = service.UploadProductAsync(model, testDb.User.Id);

            Assert.That(actual.Result, Is.EqualTo(false));
        }

        [Test]
        //Test that the method returns false if the userId parameter is null.
        public void GetUserData_Should_Return_False_If_The_UserId_Is_Null()
        {
            var model = testDb.productModel;

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
            IUploadService service = new UploadService(repoMock.Object, fileService, userManager.Object);

            var actual = service.UploadProductAsync(model, String.Empty);

            Assert.That(actual.Result, Is.EqualTo(false));
        }

        [Test]
        //Test that the method returns false if the desiredCategory variable is null.
        public void GetUserData_Should_Return_False_If_The_DesiredCategory_Is_Null_Or_Empty()
        {
            var model = testDb.productModel;
            model.Category = String.Empty;

            repoMock = new Mock<IRepository>();
            IUploadService service = new UploadService(repoMock.Object, fileService, userManager.Object);

            var actual = service.UploadProductAsync(model, testDb.User.Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.EqualTo(false));
        }

        [Test]
        //Test that the method returns false if the desiredUser variable is null.
        public void GetUserData_Should_Return_False_If_The_DesiredUser_Is_Null_Or_Empty()
        {
            var model = testDb.productModel;

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
            IUploadService service = new UploadService(repoMock.Object, fileService, userManager.Object);

            var actual = service.UploadProductAsync(model, "invalid userId");

            Assert.That(actual.Result, Is.EqualTo(false));
        }

        [Test]
        //Test that the method returns false if an exception is thrown when adding the contentEntity to the repository and saving changes.
        public void GetUserData_Should_Return_False_If_An_Exception_Is_Thrown_When_Adding_The_Invalid_ContentEntity()
        {
            var model = testDb.productModel;
            model.PhotosZip = new FormFileCollection();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
            IUploadService service = new UploadService(repoMock.Object, fileService, userManager.Object);

            var actual = service.UploadProductAsync(model, testDb.User.Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.EqualTo(false));
        }

        [Test]
        //Test that the method returns false if an exception is thrown when adding the productEntity to the repository and saving changes.
        public void GetUserData_Should_Return_False_If_An_Exception_Is_Thrown_When_Adding_The_Invalid_ProductEntity()
        {
            var model = testDb.productModel;
            model.CoverPhoto = new FormFileCollection();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Category>()).Returns(categories.BuildMock());
            IUploadService service = new UploadService(repoMock.Object, fileService, userManager.Object);

            var actual = service.UploadProductAsync(model, testDb.User.Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.EqualTo(false));
        }
    }
}
