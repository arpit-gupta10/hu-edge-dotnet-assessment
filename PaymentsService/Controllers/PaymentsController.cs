using Microsoft.AspNetCore.Mvc;
using PaymentsService.Commands;
using PaymentsService.Queries;

namespace PaymentsService.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly CreatePaymentHandler _createHandler;
    private readonly GetPaymentStatusHandler _queryHandler;

    public PaymentsController(
        CreatePaymentHandler createHandler,
        GetPaymentStatusHandler queryHandler)
    {
        _createHandler = createHandler;
        _queryHandler = queryHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
    {
        var id = await _createHandler.HandleAsync(request.OrderId, request.Amount);
        return Ok(new { PaymentId = id });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetStatus(Guid orderId)
    {
        var result = await _queryHandler.HandleAsync(orderId);
        return result == null ? NotFound() : Ok(result);
    }
}

public record CreatePaymentRequest(Guid OrderId, decimal Amount);
