using AutoMapper;
using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMapper _mapper;
    /*    private readonly ILogger<MenuItemsController> _logger;*/

    public MenuItemsController(DataContext context, IMenuItemRepository menuItemRepository, IMapper mapper /*ILogger<MenuItemsController> logger*/)
    {
        _context = context;
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
        /*        _logger = logger;*/
    }

    [HttpGet("GetMenuItems")]
    public async Task<IActionResult> GetMenuItems()
    {
        var menuItems = await _menuItemRepository.GetAll();
        return Ok(menuItems);
    }

    [HttpGet("GetSingleMenuItem/{menuId}")]
    public async Task<IActionResult> GetSingleMenuItem(int menuId)
    {
        var menuItem = await _menuItemRepository.GetById(menuId);
        if (menuItem == null)
        {
            return NotFound("MenuItem not found");
        }
        return Ok(menuItem);
    }

    [HttpPost("AddMenuItem")]
    public async Task<IActionResult> AddMenuItem(MenuItemDTO menuItemDTO)
    {
        var restaurant = await _context.Restaurants.FindAsync(menuItemDTO.RestaurantId);
        var category = await _context.Categories.FindAsync(menuItemDTO.CategoryId);
        if (restaurant == null)
        {
            return NotFound("Restaurant not found");
        }

        var menuItem = _mapper.Map<MenuItem>(menuItemDTO);
        menuItem.Restaurant = restaurant;
        menuItem.Category = category;

        // Ensure that MenuItemId is not being set here
        // Debug or log menuItem to check if MenuItemId is populated

        await _menuItemRepository.Add(menuItem);

        // Return the created MenuItem object
        return CreatedAtAction(nameof(GetSingleMenuItem), new { menuId = menuItem.MenuItemId }, menuItem);
    }



    [HttpPut("UpdateMenuItem/{menuId}")]
    public async Task<IActionResult> UpdateMenuItem(int menuId, MenuItemDTO menuItemDto)
    {
        try
        {
            var existingMenuItem = await _menuItemRepository.GetById(menuId);
            if (existingMenuItem == null)
            {
                return NotFound("MenuItem not found");
            }

            _mapper.Map(menuItemDto, existingMenuItem);

            await _menuItemRepository.Update(existingMenuItem, menuId);

            return Ok(existingMenuItem);
        }
        catch (DbUpdateException ex)
        {
            /*            _logger.LogError(ex, "Error occurred while updating a menu item.");*/
            return StatusCode(500, "An error occurred while updating the entity.");
        }
    }

    [HttpDelete("DeleteMenuItem/{menuItemId}")]
    public async Task<IActionResult> DeleteMenuItem(int menuItemId)
    {
        var menuItem = await _menuItemRepository.GetById(menuItemId);
        if (menuItem == null)
        {
            return NotFound("MenuItem not found");
        }

        await _menuItemRepository.Delete(menuItemId);
        return Ok(new { message = "MenuItem was deleted", menuItem });
    }
}
