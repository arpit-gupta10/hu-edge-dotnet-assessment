using System;
using System.Collections.Generic;
using System.Text;

namespace Payments_Core.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
    }

}
