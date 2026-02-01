using Payments_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payments_Core.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(string orderId);
        Task<Payment?> GetPaymentByIdAsync(Guid id);
    }

}
