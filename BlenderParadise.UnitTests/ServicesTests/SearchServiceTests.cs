using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderParadise.UnitTests.ServicesTests
{
    [TestFixture]
    public class SearchServiceTests : TestsBase
    {
        [Test]
        public void SearchProductAsync_Should_Return_List_Of_Products()
        {
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Product>()).Returns(products.BuildMock());
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));

            ISearchService service = new SearchService(repoMock.Object);

            var actual = service.SearchProductAsync("Product");

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<List<ViewProductModel>>(actual.Result);
            Assert.That(actual.Result, Has.Count.EqualTo(4));
        }
    }
}
