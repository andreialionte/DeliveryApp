using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user, int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Address = user.Address;
            existingUser.Role = user.Role;
            existingUser.City = user.City;
            existingUser.Region = user.Region;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Return the updated user entity
            return existingUser;
        }

        public async Task<User> Delete(int userID)
        {
            var user = _context.Users.Find(userID);
            if (user == null)
            {
                throw new Exception("Entity not exist");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
