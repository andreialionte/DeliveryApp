using DeliveryApp.API.DataLayers;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> Add(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(Order order, int id)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                throw new Exception("Order not found");
            }

            existingOrder.User = order.User;
            existingOrder.Restaurant = order.Restaurant;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.DeliveryAddress = order.DeliveryAddress;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.OrderItems = order.OrderItems;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<Order> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
