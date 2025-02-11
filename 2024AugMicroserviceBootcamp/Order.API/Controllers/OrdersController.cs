using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Order;
using Order.Application.Order.CreateOrderUseCase;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateRequest orderCreateRequest)
        {
            await orderService.CreateOrder(orderCreateRequest);
            return Ok();
        }
    }
}
