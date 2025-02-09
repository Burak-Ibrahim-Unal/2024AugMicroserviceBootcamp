using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
    }
}
