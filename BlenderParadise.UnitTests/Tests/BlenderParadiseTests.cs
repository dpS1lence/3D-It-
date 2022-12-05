using BlenderParadise.Data.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using BlenderParadise.UnitTests.Mocks;
using BlenderParadise.UnitTests.Tests;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BlenderParadise.UnitTests
{
    public class BlenderParadiseTests : TestsBase
    {
        [Test]
        public void ProductService_GetAllAsync_Should_Get_All_Products()
        {
            var allProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Product1", Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10, Photo = Array.Empty<byte>() },
                new Product { Id = 1, Name = "Product2", Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10, Photo = Array.Empty<byte>() },
                new Product { Id = 1, Name = "Product3", Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10, Photo = Array.Empty<byte>() },
                new Product { Id = 1, Name = "Product4", Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10, Photo = Array.Empty<byte>() },
            };

            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.All<Product>()).Returns(allProducts.AsQueryable());
            IProductService service = new ProductService(repoMock.Object, this.userManager.Object);

            var actual = service.GetAllAsync();

            repoMock.VerifyAll();
            Assert.That(actual.Result, Is.Not.Null);
            Assert.IsAssignableFrom<List<Product>>(actual.Result);
            Assert.That(actual.Result, Has.Count.EqualTo(4));
        }
    }
}