using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("GetSingleUser")]
        public async Task<IActionResult> GetSingleUser(int userId)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
            return Ok(user);
        }

        [HttpPost("ÄddUsers")]
    }
}
