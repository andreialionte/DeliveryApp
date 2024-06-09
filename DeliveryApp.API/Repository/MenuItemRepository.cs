using DeliveryApp.API.DataLayers;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DataContext _context;

        public MenuItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAll()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItem> GetById(int id)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == id);
        }

        public async Task<MenuItem> Add(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<MenuItem> Update(MenuItem menuItem, int menuId)
        {
            var existingMenuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuId);
            if (existingMenuItem == null)
            {
                throw new Exception("Menu item not found");
            }

            existingMenuItem.Name = menuItem.Name;
            existingMenuItem.Description = menuItem.Description;
            existingMenuItem.Price = menuItem.Price;
            existingMenuItem.Category = menuItem.Category;
            existingMenuItem.RestaurantId = menuItem.RestaurantId;

            await _context.SaveChangesAsync();
            return existingMenuItem;
        }

        public async Task<MenuItem> Delete(int menuId)
        {
            var existingMenuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuId);
            if (existingMenuItem == null)
            {
                throw new Exception("Menu item not found");
            }

            _context.MenuItems.Remove(existingMenuItem);
            await _context.SaveChangesAsync();
            return existingMenuItem;
        }
    }
}
