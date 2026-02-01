using Fulfilment_Core.Entities;
using Fulfilment_Core.Interfaces;
using Fulfilment_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfilment_Infrastructure.Services
{
    public class FulfilmentService : IFulfilmentService
    {
        private readonly FulfilmentDbContext _db;

        public FulfilmentService(FulfilmentDbContext db)
        {
            _db = db;
        }

        public async Task<FulfilmentOrder> CreateAsync(FulfilmentOrder fulfilment)
        {
            _db.Fulfilments.Add(fulfilment);
            await _db.SaveChangesAsync();
            return fulfilment;
        }

        public async Task<FulfilmentOrder?> GetByOrderIdAsync(string orderId)
        {
            return await _db.Fulfilments
                            .FirstOrDefaultAsync(f => f.OrderId == orderId);
        }

        public async Task<IEnumerable<FulfilmentOrder>> GetAllAsync()
        {
            return await _db.Fulfilments
                            .OrderByDescending(f => f.CreatedAt)
                            .ToListAsync();
        }
    }

}
