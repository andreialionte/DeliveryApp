using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public UsersController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            /*IEnumerable<User> users = await _context.Users.ToListAsync();*/
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("GetSingleUser")]
        public async Task<IActionResult> GetSingleUser(int userId)
        {
            /*User? user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);*/
            var user = _userRepository.GetById(userId);
            return Ok(user);
        }

        [HttpPost("AddUsers")]
        public async Task<IActionResult> AddUsers(UserDTO userDto)
        {
            if (userDto.Email != null)
            {
                throw new Exception("The user already exist");
            }


            User? user = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Role = userDto.Role,
                Address = userDto.Address,
                PostalCode = userDto.PostalCode,
                City = userDto.City,
                Region = userDto.Region,
            };

            await _userRepository.Add(user);
            /*            await _context.SaveChangesAsync();
            */
            return Ok(new { Message = "The user was created", User = user });
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int userId, UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                // Handle the case when the user does not exist
                throw new Exception("The user dosent exist");
            }

            // Update user properties
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.Address = userDTO.Address;
            user.Role = userDTO.Role;
            user.City = userDTO.City;
            user.Region = userDTO.Region;

            // Save changes
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "The user was updated!", User = user });
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("Something is wrong");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();


            return Ok(new { Message = "The user was deleted!", User = user });
        }
    }
}
