using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
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
            return Ok();
        }

        [HttpGet("GetSingleRestaurant")]
        public async Task<IActionResult> GetSingleRestaurant(int restaurantId)
        {
            return Ok();
        }

        [HttpPost("AddRestaurants")]
        public async Task<IActionResult> AddRestaurants(RestaurantDTO restaurantDto)
        {
            return Ok();
        }

        [HttpPut("UpdateRestaurants")]
        public async Task<IActionResult> UpdateRestaurants(RestaurantDTO restaurantDto, int restaurantId)
        {
            return Ok();
        }

        [HttpDelete("DeleteRestaurants")]
        public async Task<IActionResult> DeleteRestaurants(int restaurantId)
        {
            return Ok();
        }
    }
}
