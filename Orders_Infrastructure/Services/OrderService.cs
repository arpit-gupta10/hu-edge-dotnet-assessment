using Microsoft.EntityFrameworkCore;
using Orders_Core.Entities;
using Orders_Core.Interfaces;
using Orders_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders_Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int page, int pageSize)
        {
            return await _dbContext.Orders
                                   .OrderByDescending(o => o.CreatedAt)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}


