using OrdersService.Domain;
using OrdersService.Infrastructure;

namespace OrdersService.Commands;

public class CreateOrderHandler
{
    private readonly OrdersDbContext _db;

    public CreateOrderHandler(OrdersDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreateOrderCommand cmd)
    {
        var order = new Order(cmd.CustomerId, cmd.Amount);

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return order.Id;
    }
}
