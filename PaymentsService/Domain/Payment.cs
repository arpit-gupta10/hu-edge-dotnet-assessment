namespace PaymentsService.Domain;

public class Payment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public byte[] RowVersion { get; private set; }

    private Payment() { }

    public Payment(Guid orderId, decimal amount)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Amount = amount;
        Status = "Initiated";
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkSuccess()
    {
        Status = "Success";
    }

    public void MarkFailed()
    {
        Status = "Failed";
    }
}
