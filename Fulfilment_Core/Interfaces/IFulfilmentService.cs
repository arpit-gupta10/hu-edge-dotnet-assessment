using Fulfilment_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfilment_Core.Interfaces
{
    public interface IFulfilmentService
    {
        Task<FulfilmentOrder> CreateAsync(FulfilmentOrder fulfilment);
        Task<FulfilmentOrder?> GetByOrderIdAsync(string orderId);
        Task<IEnumerable<FulfilmentOrder>> GetAllAsync();
    }

}
