using BlenderParadise.Data.Models;
using BlenderParadise.Models;
using Microsoft.AspNetCore.Http;

namespace BlenderParadise.Tests.Common
{
    public class BlenderParadiseTestDb
    {
        public BlenderParadiseTestDb()
        {
            User.ProductsData = products;
            User2.ProductsData = products;
        }

        public ApplicationUser User = new()
        {
            Id = "1",
            UserName = "Pesho",
            Description = "Descr"
        };
        public ApplicationUser UserWithNoUploads = new()
        {
            Id = "2",
            UserName = "Pesho",
            Description = "Descr",
        };
        public ApplicationUser User2 = new()
        {
            Id = "3",
            UserName = "Pesho",
            Description = "Descr"
        };

        public string File = "str";
        public string File2 = "str2";
        public string File3 = "str3";
        public string File4 = "str4";
        //public ApplicationUser B { get; set; }

        public readonly List<Product> products = new()
           {
               new Product { Id = 1, Name = "Product1", CategoryId = 1, Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10,Photo = Array.Empty<byte>(), UserId =  "1", ContentId = 2 },
               new Product { Id = 2, Name = "Product2", CategoryId = 2, Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10,Photo = Array.Empty<byte>(), UserId =  "3", ContentId = 3 },
               new Product { Id = 3, Name = "Product3", CategoryId = 3, Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10,Photo = Array.Empty<byte>(), UserId =  "3", ContentId = 4 },
               new Product { Id = 4, Name = "Product4", CategoryId = 4, Description = "Description", Geometry = 10, Polygons = 10, Vertices = 10,Photo = Array.Empty<byte>(), UserId =  "1", ContentId = 1}
           };

        public readonly List<Content> content = new()
            {
                new Content { Id = 1, PhotosZip = Array.Empty<byte>(), FileName = "str" },
                new Content { Id = 2, PhotosZip = Array.Empty<byte>(), FileName = "str2" },
                new Content { Id = 3, PhotosZip = Array.Empty<byte>(), FileName = "str3" },
                new Content { Id = 4, PhotosZip = Array.Empty < byte >(), FileName = "str4" }
            };

        public readonly List<ApplicationUser> users = new()
            {
                new ApplicationUser
                {
                    Id = "3",
                    UserName = "Pesho",
                    Description = "Descr"
                },
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "Pesho",
                    Description = "Descr"
                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "Pesho",
                    Description = "Descr"
                }
            };

        public readonly List<Category> categories = new()
            {
                new Category { Id = 1, Name = "Category1" },
                new Category { Id = 2, Name = "Category2" },
                new Category { Id = 3, Name = "Category3" },
                new Category { Id = 4, Name = "Category4" }
            };

        public readonly List<Photo> photos = new()
            {
                new Photo { Id = 1, PhotoFile = Array.Empty<byte>(), ProductId = 1 },
                new Photo { Id = 2, PhotoFile = Array.Empty<byte>(), ProductId = 1 },
                new Photo { Id = 3, PhotoFile = Array.Empty<byte>(), ProductId = 1 },
                new Photo { Id = 4, PhotoFile = Array.Empty<byte>(), ProductId = 2 }
            };

        public ProductModel productModel = new()
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
    }
}