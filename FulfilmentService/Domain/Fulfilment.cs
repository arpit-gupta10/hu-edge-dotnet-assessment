namespace FulfilmentService.Domain;

public class Fulfilment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public byte[] RowVersion { get; private set; }

    private Fulfilment() { }

    public Fulfilment(Guid orderId)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = "Started";
        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = "Completed";
    }
}
