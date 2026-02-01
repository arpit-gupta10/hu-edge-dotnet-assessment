using Microsoft.EntityFrameworkCore;
using Payments_Core.Entities;
using Payments_Core.Interfaces;
using Payments_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payments_Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentDbContext _dbContext;

        public PaymentService(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            _dbContext.Payments.Add(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(string orderId)
        {
            return await _dbContext.Payments
                                   .Where(p => p.OrderId == orderId)
                                   .OrderByDescending(p => p.PaidAt)
                                   .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(Guid id)
        {
            return await _dbContext.Payments.FirstOrDefaultAsync(p => p.Id == id);
        }
    }

}
