using System;
using System.Collections.Generic;
using System.Text;

namespace Fulfilment_Core.Entities
{
    public class FulfilmentOrder
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // Pending, Shipped, Delivered
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
