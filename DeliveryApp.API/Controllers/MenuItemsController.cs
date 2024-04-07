using DeliveryApp.API.DataLayers;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public MenuItemsController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("GetMenuItems")]
        public async Task<IActionResult> GetMenuItems()
        {
            return Ok();
        }

        [HttpGet("GetSingleMenuItem")]
        public async Task<IActionResult> GetSingleMenuItem()
        {
            return Ok();
        }

        [HttpPost("AddMenuItems")]
        public async Task<IActionResult> AddMenuItems()
        {
            return Ok();
        }

        [HttpPut("UpdateMenuItems")]
        public async Task<IActionResult> UpdateMenuItems()
        {
            return Ok();
        }

        [HttpDelete("DeleteMenuItems")]
        public async Task<IActionResult> DeleteMenuItems()
        {
            return Ok();
        }
    }
}
