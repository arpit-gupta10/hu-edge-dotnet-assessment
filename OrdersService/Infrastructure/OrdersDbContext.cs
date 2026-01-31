using Microsoft.EntityFrameworkCore;
using OrdersService.Domain;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrdersService.Infrastructure;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(b =>
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.TotalAmount).HasPrecision(18,2);
            b.Property(o => o.Status).IsRequired();
            b.Property(o=>o.CreatedAt).IsRequired();
            b.Property(o => o.RowVersion)
             .IsRowVersion();
        });
    }
}
