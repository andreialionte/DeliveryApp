using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
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
            var orders = await _orderRepository.GetAll();
            return Ok(orders);
        }

        [HttpGet("GetSingleOrder")]
        public async Task<IActionResult> GetSingleOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            return Ok(order);
        }

        [HttpPost("AddOrders")]
        public async Task<IActionResult> AddOrders(OrderDTO orderDto)
        {
            Order order = new Order()
            {
                OrderDate = orderDto.OrderDate,
                DeliveryAddress = orderDto.DeliveryAddress,
                TotalAmount = orderDto.TotalAmount
            };
            
            var newOrder = await _orderRepository.Add(order);
            return Ok(newOrder);
            //we need to handle the total amount based of orderitem like what we select to order and how much and after
            //after we place the order we can calculate the total amount
        }

        [HttpPut("UpdateOrders")]
        public async Task<IActionResult> UpdateOrders(OrderDTO orderDto, int orderId)
        {
            var existingOrder = await  _orderRepository.GetById(orderId);
            if(existingOrder == null)
            {
                throw new Exception("Order not found");
            }
            existingOrder.OrderDate = orderDto.OrderDate;
            existingOrder.DeliveryAddress = orderDto.DeliveryAddress;
            existingOrder.TotalAmount = orderDto.TotalAmount;
            await _orderRepository.Update(existingOrder, orderId);
            return Ok();
        }

        [HttpDelete("DeleteOrders")]
        public async Task<IActionResult> DeleteOrders(int orderId)
        {
            var orders = await _orderRepository.Delete(orderId);
            return Ok(orders);
        }
    }
}
