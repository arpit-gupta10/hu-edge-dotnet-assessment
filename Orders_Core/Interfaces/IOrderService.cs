using Orders_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders_Core.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int page, int pageSize);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(Guid id);
    }
}
