using Microsoft.EntityFrameworkCore;
using PaymentsService.Domain;

namespace PaymentsService.Infrastructure;

public class PaymentsDbContext : DbContext
{
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
        : base(options) { }

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(b =>
        {
            b.HasKey(p => p.Id);

            b.Property(p => p.Amount)
             .HasPrecision(18, 2);

            b.Property(p => p.Status)
             .IsRequired();

            b.Property(p => p.CreatedAt)
             .IsRequired();

            b.Property(p => p.RowVersion)
             .IsRowVersion();
        });
    }
}
