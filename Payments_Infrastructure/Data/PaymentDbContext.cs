using Microsoft.EntityFrameworkCore;
using Payments_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payments_Infrastructure.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        public DbSet<Payment> Payments => Set<Payment>();
    }

}
