using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;


        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet("GetRestaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAll();
            return Ok(restaurants);
        }

        [HttpGet("GetSingleRestaurant")]
        public async Task<IActionResult> GetSingleRestaurant(int restaurantId)
        {
            var restaurants = await _restaurantRepository.GetById(restaurantId);
            return Ok(restaurants);
        }

        [HttpPost("AddRestaurants")]
        public async Task<IActionResult> AddRestaurants(RestaurantDTO restaurantDto)
        {
            var restaurant = new Restaurant()
            {
                Name = restaurantDto.Name,
                Address = restaurantDto.Address,
                PhoneNumber = restaurantDto.PhoneNumber
            };  
            var newRestaurant = await _restaurantRepository.Add(restaurant);
            return Ok(newRestaurant);
        }

        [HttpPut("UpdateRestaurants")]
        public async Task<IActionResult> UpdateRestaurants(RestaurantDTO restaurantDto, int restaurantId)
        {
            var existingRestaurant = await _restaurantRepository.GetById(restaurantId);

            if (existingRestaurant == null)
            {
                throw new Exception("Restaurant not found");
            }
            
            existingRestaurant.Name = restaurantDto.Name;
            existingRestaurant.Address = restaurantDto.Address;
            existingRestaurant.PhoneNumber = restaurantDto.PhoneNumber;

            await _restaurantRepository.Update(existingRestaurant, restaurantId);

            return Ok(existingRestaurant);
        }

        [HttpDelete("DeleteRestaurants")]
        public async Task<IActionResult> DeleteRestaurants(int restaurantId)
        {
            var restaurants = await _restaurantRepository.Delete(restaurantId);
            return Ok(restaurants);
        }
    }
}
