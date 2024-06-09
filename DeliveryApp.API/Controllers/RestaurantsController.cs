using AutoMapper;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryApp.API.Services;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;


        public RestaurantsController(IRestaurantRepository restaurantRepository, IMapper mapper, IFileService fileService)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _fileService = fileService;
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
            /*            if (restaurantDto.Photo == null)
                        {
                            return BadRequest("Photo is required.");
                        }*/

            var restaurant = _mapper.Map<Restaurant>(restaurantDto);

            var photoUrl = await _fileService.Upload(restaurantDto.formFile, "RestaurantBgPhotos/");
            restaurant.RestaurantPhotoUrl = photoUrl;

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
            /*            existingRestaurant.Description = restaurantDto.Description;*/
            existingRestaurant.Address = restaurantDto.Address;
            existingRestaurant.Email = restaurantDto.Email;
            existingRestaurant.PhoneNumber = restaurantDto.PhoneNumber;
            /*            existingRestaurant.DeliveryFee = restaurantDto.DeliveryFee;*/
            existingRestaurant.OperatingHours = restaurantDto.OperatingHours;

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
