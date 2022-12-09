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
    public class DownloadServiceTests : TestsBase
    {
        private readonly BlenderParadiseTestDb testDb = new();

        [Test]
        public void DownloadModelAsync_Should_Return_Desired_Model_Name()
        {
/*            var products = testDb.products.ToList();

            var content = testDb.content.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Content>(It.IsAny<int>()))!.ReturnsAsync((int id) => content.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));

            IDownloadService service = new DownloadService(repoMock.Object, this.userManager.Object);

            var actual = service.DownloadModelAsync(products.First().Id);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<List<ViewProductModel>>(actual.Result);
            Assert.That(actual.Result, Is.EqualTo("Product1"));*/
        }
    }
}
