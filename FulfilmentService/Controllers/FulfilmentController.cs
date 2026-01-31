using Microsoft.AspNetCore.Mvc;
using FulfilmentService.Domain;
using FulfilmentService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FulfilmentService.Controllers;

[ApiController]
[Route("api/fulfilment")]
public class FulfilmentController : ControllerBase
{
    private readonly FulfilmentDbContext _db;

    public FulfilmentController(FulfilmentDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Start([FromBody] StartFulfilmentRequest request)
    {
        var fulfilment = new Fulfilment(request.OrderId);
        _db.Fulfilments.Add(fulfilment);
        await _db.SaveChangesAsync();
        return Ok(fulfilment.Id);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        var result = await _db.Fulfilments
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.OrderId == orderId);

        return result is null ? NotFound() : Ok(result);
    }
}

public record StartFulfilmentRequest(Guid OrderId);
