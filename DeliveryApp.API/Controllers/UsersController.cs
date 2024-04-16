using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public UsersController(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAll(); // Await the async method
            return Ok(users);
        }

        [HttpGet("GetSingleUser")]
        public async Task<IActionResult> GetSingleUser(int userId)
        {
            var user = await _userRepository.GetById(userId); // Await the async method
            return Ok(user);
        }

        [HttpPost("AddUsers")]
        public async Task<IActionResult> AddUsers(UserDTO userDto)
        {
            /*            if (userDto.Email != null)
                        {
                            throw new Exception("The user already exists");
                        }*/

            User user = new User()
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
            return Ok(new { Message = "The user was created", User = user });
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int userId, UserDTO userDTO)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("The user doesn't exist");
            }

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.Address = userDTO.Address;
            user.Role = userDTO.Role;
            user.City = userDTO.City;
            user.Region = userDTO.Region;

            await _userRepository.Update(user, userId); // Await the async method

            return Ok(new { Message = "The user was updated!", User = user });
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _userRepository.GetById(userId); // Await the async method
            if (user == null)
            {
                throw new Exception("The user doesn't exist");
            }

            await _userRepository.Delete(userId); // Await the async method

            return Ok(new { Message = "The user was deleted!", User = user });
        }
    }
}
