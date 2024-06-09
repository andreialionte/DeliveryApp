using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemsController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet("GetOrderItems")]
        public async Task<IActionResult> GetOrderItems()
        {
            var orderItems = await _orderItemRepository.GetAll();
            if (orderItems == null)
            {
                throw new Exception("Order Items not found");
            }
            return Ok(orderItems);
        }

        [HttpGet("GetSingleOrderItem")]
        public async Task<IActionResult> GetSingleOrderItem(int orderItemId)
        {
            var orderItems = await _orderItemRepository.GetById(orderItemId);
            if (orderItems == null)
            {
                throw new Exception($"Order Item with Id ({orderItems.OrderId}) not found");
            }
            return Ok(orderItems);
        }

        [HttpPost("AddOrderItems")]
        public async Task<IActionResult> AddOrderItems(OrderItemDTO orderItemdTo)
        {
            var orderItem = new OrderItem()
            {
                Quantity = orderItemdTo.Quantity,
                TotalPrice = orderItemdTo.TotalPrice
            };
            var newOrderItem = _orderItemRepository.Add(orderItem);
            return Ok(newOrderItem);
        }

        [HttpPut("UpdateOrderItems")]
        public async Task<IActionResult> UpdateOrderItems()
        {
            return Ok();

        }

        [HttpDelete("DeleteOrderItems")]
        public async Task<IActionResult> DeleteOrderItems(int orderId)
        {
            var orderItems = await _orderItemRepository.Delete(orderId);
            return Ok(orderItems);
        }
    }
}
