using Fulfilment_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfilment_Infrastructure.Data
{
    public class FulfilmentDbContext : DbContext
    {
        public FulfilmentDbContext(DbContextOptions<FulfilmentDbContext> options)
            : base(options) { }

        public DbSet<FulfilmentOrder> Fulfilments => Set<FulfilmentOrder>();
    }

}
