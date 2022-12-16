using BlenderParadise.Data.Models;
using BlenderParadise.Models.Product;

namespace BlenderParadise.UnitTests.ServicesTests
{
    [TestFixture]
    public class ProductServiceTests : TestsBase
    {
        [Test]
        public void GetAllAsync_Should_Get_All_Products()
        {
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Product>()).Returns(products.AsQueryable());
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            var actual = service.GetAllAsync();

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<List<ViewProductModel>>(actual.Result);
            Assert.That(actual.Result, Has.Count.EqualTo(4));

            for (int i = 0; i < actual.Result.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual.Result[i].Id, Is.EqualTo(products[i].Id));
                    Assert.That(actual.Result[i].Name, Is.EqualTo(products[i].Name));
                    Assert.That(actual.Result[i].UserName, Is.EqualTo(testDb.users.FirstOrDefault(a => a.Id == products[i].UserId)?.UserName));
                    Assert.That(actual.Result[i].Photo, Is.EqualTo("data:image/jpg;base64,"));
                    Assert.That(actual.Result[i].UserPhoto, Is.EqualTo(products[i].ApplicationUser?.ProfilePicture));
                    Assert.That(actual.Result[i].Category, Is.EqualTo(categories.FirstOrDefault(a => a.Id == products[i].CategoryId)?.Name));
                    Assert.That(actual.Result[i].Description, Is.EqualTo(products[i].Description));
                });
            }
        }

        [Test]
        public void GetAllAsync_Should_Throw_ArgumentException_When_DesiredCategory_Is_Null()
        {
            var product = new Product
            {
                Id = 1,
                Description = "Test",
                CategoryId = -1
            };

            var categories = testDb.categories.ToList();

            var photos = testDb.photos.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Product>() { product }.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.All<Product>()).Returns(new List<Product>() { product }.AsQueryable());

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetAllAsync());
        }

        [Test]
        public void GetOneAsync_Should_Get_One_Product()
        {
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            var photos = testDb.photos.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.All<Photo>()).Returns(photos.AsQueryable());

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            var actual = service.GetOneAsync(1);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<DownloadProductModel>(actual.Result);

            Assert.Multiple(() =>
            {
                Assert.That(actual.Result.Id, Is.EqualTo(testDb.products.First().Id));
                Assert.That(actual.Result.Name, Is.EqualTo(testDb.products.First().Name));
                Assert.That(actual.Result.Description, Is.EqualTo(testDb.products.First().Description));
                Assert.That(actual.Result.Polygons, Is.EqualTo(testDb.products.First().Polygons));
                Assert.That(actual.Result.Vertices, Is.EqualTo(testDb.products.First().Vertices));
                Assert.That(actual.Result.Geometry, Is.EqualTo(testDb.products.First().Geometry));
                Assert.That(actual.Result.UserId, Is.EqualTo(testDb.products.First().UserId));
                Assert.That(actual.Result.UserName, Is.EqualTo(testDb.users.FirstOrDefault(a => a.Id == testDb.products.First().UserId)?.UserName));
                Assert.That(actual.Result.Category, Is.EqualTo(testDb.categories.FirstOrDefault(a => a.Id == testDb.products.First().CategoryId)?.Name));
                Assert.That(actual.Result.CoverPhoto, Is.EqualTo("data:image/jpg;base64,"));
                Assert.That(actual.Result.Photos, Has.Count.EqualTo(photos.Where(a => a.ProductId == products.First().Id).ToList().Count));
                foreach (var item in actual.Result.Photos)
                {
                    Assert.That(item, Is.EqualTo("data:image/jpg;base64,"));
                }
            });
        }

        [Test]
        public void GetOneAsync_Should_Throw_ArgumentException_When_ProductEntity_Is_Null()
        {
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            var photos = testDb.photos.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => products.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.All<Photo>()).Returns(photos.AsQueryable());

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetOneAsync(-1));
        }

        [Test]
        public void GetOneAsync_Should_Throw_ArgumentException_When_DesiredCategory_Is_Null()
        {
            var product = new Product
            {
                Id = 1,
                Description = "Test",
                CategoryId = -1
            };

            var categories = testDb.categories.ToList();

            var photos = testDb.photos.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Product>() { product }.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetOneAsync(1));
        }

        [Test]
        public void GetOneAsync_Should_Throw_ArgumentException_When_ProductPhotos_Is_Null()
        {
            var product = new Product
            {
                Id = 10,
                Description = "Test",
                CategoryId = 1
            };

            var categories = testDb.categories.ToList();

            var photos = testDb.photos.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Product>() { product }.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.All<Photo>()).Returns(photos.AsQueryable());

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetOneAsync(10));
        }

        [Test]
        public void GetOneAsync_Should_Throw_ArgumentException_When_User_Is_Null()
        {
            var product = new Product
            {
                Id = 1,
                Description = "Test",
                CategoryId = 1,
                UserId = "-1"
            };

            var categories = testDb.categories.ToList();

            var photos = testDb.photos.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetByIdAsync<Product>(It.IsAny<int>()))!.ReturnsAsync((int id) => new List<Product>() { product }.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));
            repoMock.Setup(r => r.All<Photo>()).Returns(photos.AsQueryable());

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetOneAsync(1));
        }

        [Test]
        public void SearchProductAsync_Should_Get_All_Matching_Products()
        {
            var products = testDb.products.ToList();

            var categories = testDb.categories.ToList();

            repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Product>()).Returns(products.AsQueryable());
            repoMock.Setup(r => r.GetByIdAsync<Category>(It.IsAny<int>()))!.ReturnsAsync((int id) => categories.FirstOrDefault(a => a.Id == id));

            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            var actual = service.SearchProductAsync(products.First().Name);

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<List<ViewProductModel>>(actual.Result);
            Assert.That(actual.Result, Has.Count.EqualTo(products.Where(a => a.Name == products.First().Name).ToList().Count));
        }
    }
}