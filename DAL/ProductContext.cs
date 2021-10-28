using Microsoft.EntityFrameworkCore;
using System;
using WidgetAndCo.Models;

namespace WidgetAndCo.DAL
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultContainer("Store");

            modelBuilder.Entity<Product>().ToContainer("Products")
            .OwnsMany(p => p.Reviews);
        }
    }
}
