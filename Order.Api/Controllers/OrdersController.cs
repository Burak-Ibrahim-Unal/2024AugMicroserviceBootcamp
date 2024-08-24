using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Service;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService _orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _orderService.Create();
            return Ok();
        }
    }
}
