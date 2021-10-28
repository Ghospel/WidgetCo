using Microsoft.EntityFrameworkCore;
using System;
using WidgetAndCo.Models;

namespace WidgetAndCo.DAL
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultContainer("Store");

            modelBuilder.Entity<Order>().ToContainer("Orders")
            .OwnsOne(o => o.Customer,
            c =>
            {
                c.ToJsonProperty("Customer");
            })

            .OwnsMany(p => p.Products,
            p =>
            {
                p.OwnsMany(r => r.Reviews);
            })

            .Property(o => o.ShippingDate).HasDefaultValue(null);

        }
    }
}
