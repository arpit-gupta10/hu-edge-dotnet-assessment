using Microsoft.AspNetCore.Mvc;
using OrdersService.Commands;
using OrdersService.Queries;

namespace OrdersService.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderHandler _createHandler;
    private readonly GetOrdersHandler _readHandler;

    public OrdersController(
        CreateOrderHandler createHandler,
        GetOrdersHandler readHandler)
    {
        _createHandler = createHandler;
        _readHandler = readHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand cmd)
    {
        var id = await _createHandler.Handle(cmd);
        return Ok(new { OrderId = id });
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var result = await _readHandler.HandleAsync(
            page, pageSize);
        return Ok(result);
    }
}
