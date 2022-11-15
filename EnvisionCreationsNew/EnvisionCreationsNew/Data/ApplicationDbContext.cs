using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EnvisionCreationsNew.Data.Models;

namespace EnvisionCreationsNew.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Content> Content { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ProductContent> ProductsContent { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

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

            base.OnModelCreating(builder);
        }
    }
}