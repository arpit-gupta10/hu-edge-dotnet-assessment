namespace PaymentsService.Queries;

public class PaymentStatusReadModel
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
}
