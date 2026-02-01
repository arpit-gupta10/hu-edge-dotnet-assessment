using Fulfilment_Core.Entities;
using Fulfilment_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fulfilment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FulfilmentController : ControllerBase
    {
        private readonly IFulfilmentService _service;

        public FulfilmentController(IFulfilmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByOrderId(string orderId)
        {
            var result = await _service.GetByOrderIdAsync(orderId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FulfilmentOrder fulfilment)
        {
            var created = await _service.CreateAsync(fulfilment);
            return CreatedAtAction(nameof(GetByOrderId),
                new { orderId = created.OrderId }, created);
        }
    }

}
