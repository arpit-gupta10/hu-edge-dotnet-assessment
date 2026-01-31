using PaymentsService.Domain;
using PaymentsService.Infrastructure;

namespace PaymentsService.Commands;

public class CreatePaymentHandler
{
    private readonly PaymentsDbContext _db;

    public CreatePaymentHandler(PaymentsDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> HandleAsync(Guid orderId, decimal amount)
    {
        var payment = new Payment(orderId, amount);

        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();

        return payment.Id;
    }
}
