using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> CheckStatus(int productId,int quantity)
        {
            return Ok(new { Status = true });
        }
    }
}
