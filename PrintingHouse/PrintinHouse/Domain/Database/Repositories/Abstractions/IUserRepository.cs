using PrintingHouse.Domain.Database.Repositories.DTOs.UserDTO;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetUserByID(int userId);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<int> CreateUser(CreateUserDTO createUserDTO);
        Task<bool> updateUser(UpdateUserDTO updateUserDTO, int userID);
        Task<bool> deleteUser(int userId);
    }
}
