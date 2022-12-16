using BlenderParadise.Data.Models;

namespace BlenderParadise.UnitTests.ServicesTests
{
    [TestFixture]
    public class DownloadServiceTests : TestsBase
    {
        [Test]
        public void GetNameAsync_Should_Return_Desired_Model_Name()
        {
            var products = testDb.products.ToList();

            var content = testDb.content.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object);

            var actual = service.GetNameAsync(products.First().Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.That(actual.Result, Is.EqualTo(content.Where(a => a.Id == products.First().ContentId).First().FileName));
        }

        [Test]
        public void GetNameAsync_Should_Throw_ArgumentException_When_contentEntity_Is_Null()
        {
            var product = new Product
            {
                Id = 1,
                Description = "Test",
                ContentId = -1,
            };

            var content = testDb.content.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Product>() { product }.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetNameAsync(product.Id));
        }

        [Test] 
        public void GetZipAsync_Should_Return_Desired_Zip()
        {
            var products = testDb.products.ToList();

            var content = testDb.content.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object);

            var actual = service.GetZipAsync(products.First().Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<FileContentResult>(actual.Result);
        }

        [Test]
        public void GetZipAsync_Should_Throw_ArgumentException_When_ContentEntity_Is_Null()
        {
            var product = new Product
            {
                Id = 1,
                Description = "Test",
                ContentId = -1,
            };

            var content = testDb.content.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Product>() { product }.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetZipAsync(product.Id));
        }

        [Test]
        public void GetZipAsync_Should_Throw_ArgumentException_When_ContentEntity_Photos_Is_Null()
        {
            var product = new Product
            {
                Id = 1,
                Description = "Test",
                ContentId = 1,
            };

            var content = new Content
            {
                Id = 1,
                PhotosZip = Array.Empty<byte>(),
            };

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Content>() { content }.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Product>() { product }.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetZipAsync(product.Id));
        }
    }
}
