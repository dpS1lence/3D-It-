namespace BlenderParadise.UnitTests.ServicesTests
{
    [TestFixture]
    public class DownloadServiceTests : TestsBase
    {
        [Test]
        public void DownloadModelAsync_Should_Return_Desired_Model_Name()
        {
            var products = testDb.products.ToList();

            var content = testDb.content.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object, this.userManager.Object);

            var actual = service.DownloadModelAsync(products.First().Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.That(actual.Result, Is.EqualTo(content.Where(a => a.Id == products.First().ContentId).First().FileName));
        }

        [Test] 
        public void DownloadZipAsync_Should_Return_Desired_Zip()
        {
            var products = testDb.products.ToList();

            var content = testDb.content.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object, this.userManager.Object);

            var actual = service.DownloadZipAsync(products.First().Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<FileContentResult>(actual.Result);
        }
    }
}
