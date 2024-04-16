using DeliveryApp.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        [HttpGet("GetOrderItems")]
        public async Task<IActionResult> GetOrderItems()
        {
            return Ok();
        }

        [HttpGet("GetSingleOrderItem")]
        public async Task<IActionResult> GetSingleOrderItem()
        {
            return Ok();
        }

        [HttpPost("AddOrderItems")]
        public async Task<IActionResult> AddOrderItems()
        {
            return Ok();
        }

        [HttpPut("UpdateOrderItems")]
        public async Task<IActionResult> UpdateOrderItems()
        {
            return Ok();
        }

        [HttpDelete("DeleteOrderItems")]
        public async Task<IActionResult> DeleteOrderItems()
        {
            return Ok();
        }
    }
}
