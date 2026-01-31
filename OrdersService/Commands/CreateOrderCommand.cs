namespace OrdersService.Commands
{
    public record CreateOrderCommand(string CustomerId, decimal Amount);
}
