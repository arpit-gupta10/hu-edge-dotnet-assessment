namespace OrdersService.Domain;

public class Order
{
    public Guid Id { get; private set; }
    public string CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public byte[] RowVersion { get; private set; } // Optimistic Concurrency

    private Order() { }

    public Order(string customerId, decimal totalAmount)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        TotalAmount = totalAmount;
        Status = "Created";
        CreatedAt = DateTime.UtcNow;

    }

    public void MarkPaid()
    {
        Status = "Paid";
    }
}
