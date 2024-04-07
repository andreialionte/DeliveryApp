using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.Repository;

namespace DeliveryApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetById(userId);
        }

        public async Task<User> AddUser(User user)
        {
            return await _userRepository.Add(user);
        }

        public async Task<User> UpdateUser(User user, int userId)
        {
            return await _userRepository.Update(user, userId);
        }

        public async Task<User> DeleteUser(int userId)
        {
            return await _userRepository.Delete(userId);
        }
    }
}
