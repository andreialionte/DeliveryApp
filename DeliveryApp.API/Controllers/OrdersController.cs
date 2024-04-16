using DeliveryApp.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok();
        }

        [HttpGet("GetSingleOrder")]
        public async Task<IActionResult> GetSingleOrder()
        {
            return Ok();
        }

        [HttpPost("AddOrders")]
        public async Task<IActionResult> AddOrders()
        {
            return Ok();
        }

        [HttpPut("UpdateOrders")]
        public async Task<IActionResult> UpdateOrders()
        {
            return Ok();
        }

        [HttpDelete("DeleteOrders")]
        public async Task<IActionResult> DeleteOrders()
        {
            return Ok();
        }
    }
}
