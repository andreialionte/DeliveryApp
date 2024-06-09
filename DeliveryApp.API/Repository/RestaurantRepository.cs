using AutoMapper;
using DeliveryApp.API.DataLayers;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RestaurantRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetById(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task<Restaurant> Add(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }

        public async Task<Restaurant> Update(Restaurant restaurant, int id)
        {
            var existingRestaurant = await _context.Restaurants.FindAsync(id);
            if (existingRestaurant == null)
            {
                throw new Exception("Restaurant not found");
            }

            // Update individual properties
            existingRestaurant.Name = restaurant.Name;
            /*            existingRestaurant.Description = restaurant.Description;*/
            existingRestaurant.Address = restaurant.Address;
            existingRestaurant.Email = restaurant.Email;
            existingRestaurant.PhoneNumber = restaurant.PhoneNumber;
            /*            existingRestaurant.DeliveryFee = restaurant.DeliveryFee;*/
            existingRestaurant.OperatingHours = restaurant.OperatingHours;

            // Save changes
            await _context.SaveChangesAsync();

            return existingRestaurant;
        }

        public async Task<Restaurant> Delete(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                throw new Exception("Restaurant not found");
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return restaurant;
        }
    }
}
