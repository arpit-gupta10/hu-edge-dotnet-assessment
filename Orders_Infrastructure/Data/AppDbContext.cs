using Microsoft.EntityFrameworkCore;
using Orders_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders_Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
    }

}
