using FulfilmentService.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FulfilmentService.Infrastructure;

public class FulfilmentDbContext : DbContext
{
    public FulfilmentDbContext(DbContextOptions<FulfilmentDbContext> options)
        : base(options) { }

    public DbSet<Fulfilment> Fulfilments => Set<Fulfilment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fulfilment>(b =>
        {
            b.HasKey(f => f.Id);
            b.Property(f => f.Status).IsRequired();
            b.Property(f => f.CreatedAt).IsRequired();
            b.Property(f => f.RowVersion).IsRowVersion();
        });
    }
}
