using DeliveryApp.API.DataLayers.Entities;

namespace DeliveryApp.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user, int id);
        Task<User> DeleteUser(int userId);
    }
}
