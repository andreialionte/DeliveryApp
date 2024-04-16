using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemsController(DataContext context, IMenuItemRepository menuItemRepository)
        {
            _context = context;
            _menuItemRepository = menuItemRepository;
        }


        [HttpGet("GetMenuItems")]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await _menuItemRepository.GetAll();
            return Ok(menuItems);
        }

        [HttpGet("GetSingleMenuItem")]
        public async Task<IActionResult> GetSingleMenuItem(int menuId)
        {
            var menuItem = await _menuItemRepository.GetById(menuId);
            return Ok(menuItem);
        }

        [HttpPost("AddMenuItems")]
        public async Task<IActionResult> AddMenuItems(MenuItemDTO menuItemDTO)
        {
            MenuItem menuItem = new MenuItem()
            {
                Name = menuItemDTO.Name,
                Description = menuItemDTO.Description,
                Price = menuItemDTO.Price,
                Category = menuItemDTO.Category
            };

            await _menuItemRepository.Add(menuItem);


            return Ok(menuItem);
        }

        [HttpPut("UpdateMenuItems")]
        public async Task<IActionResult> UpdateMenuItems(MenuItemDTO menuItemDto, int menuId)
        {
            return Ok();
        }

        [HttpDelete("DeleteMenuItems")]
        public async Task<IActionResult> DeleteMenuItems(int menuItemId)
        {
            var menuitem = _menuItemRepository.GetById(menuItemId);

            await _menuItemRepository.Delete(menuItemId); //menuItem
            return Ok(new { message = "MenuItem was added", menuItem = menuitem });
        }
    }
}
