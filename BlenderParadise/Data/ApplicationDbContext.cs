using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlenderParadise.Data.Models;

namespace BlenderParadise.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Content> Content { get; set; } = null!;

        public DbSet<Challange> Challenges { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Photo> Photos { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<ProductContent> ProductsContent { get; set; } = null!;

        public DbSet<ProductPhoto> ProductPhotos { get; set; } = null!;

        public DbSet<ApplicationUserProduct> ApplicationUsersProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserProduct>().HasKey(key => new
            {
                key.ProductId,
                key.ApplicationUserId
            });

            builder.Entity<ProductContent>().HasKey(key => new
            {
                key.ProductId,
                key.ContentId
            });

            builder.Entity<ProductPhoto>().HasKey(key => new
            {
                key.ProductId,
                key.PhotoId
            });

            base.OnModelCreating(builder);
        }
    }
}