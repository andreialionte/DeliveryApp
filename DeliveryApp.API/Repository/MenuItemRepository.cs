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

        public async Task<MenuItem> Update(MenuItem menu, int menuId)
        {
            var menuitem = await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuId);
            if (menuitem != null)
            {
                throw new Exception("This item already exist");
            }
            _context.Update(menuitem);
            await _context.SaveChangesAsync();
            return menuitem;
        }

        public async Task<MenuItem> Delete(int menuId)
        {
            var menuitem = await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuId);
            if (menuitem == null)
            {
                throw new Exception("This menuItem does not exist");
            }
            _context.Remove(menuitem);
            await _context.SaveChangesAsync();
            return menuitem;
        }
    }
}
