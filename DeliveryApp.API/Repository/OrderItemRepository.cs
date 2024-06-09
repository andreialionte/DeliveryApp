using DeliveryApp.API.DataLayers;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DataContext _context;

        public OrderItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetById(int id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> Add(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> Update(OrderItem orderItem, int id)
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(id);
            if (existingOrderItem == null)
            {
                throw new Exception("Order item not found");
            }

            existingOrderItem.Order = orderItem.Order;
            /*            existingOrderItem.MenuItem = orderItem.MenuItem;*/
            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.TotalPrice = orderItem.TotalPrice;

            _context.OrderItems.Update(existingOrderItem);
            await _context.SaveChangesAsync();

            return existingOrderItem;
        }

        public async Task<OrderItem> Delete(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new Exception("Order item not found");
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }
    }
}
