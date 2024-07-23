using DomainLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data
{
    public class ECommerceDbContext : IdentityDbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
            
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasQueryFilter(brand => !brand.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(category => !category.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(product => !product.isDeleted);

            modelBuilder.Entity<Brand>()
                        .HasMany(product => product.Products)
                        .WithOne(brand => brand.Brand);

            modelBuilder.Entity<Category>()
                        .HasMany(products => products.Products)
                        .WithOne(category => category.Category);

            modelBuilder.Entity<Product>()
                        .HasOne(brand => brand.Brand)
                        .WithMany(products => products.Products);

            modelBuilder.Entity<Product>()
                        .HasOne(category => category.Category)
                        .WithMany(products => products.Products);
                        
        }
    }
}
