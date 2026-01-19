using PrintingHouse.Domain.Database.Repositories.DTOs.UserDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Users.DTOs;

namespace PrintingHouse.Infrastructure.Services.Users.Abstractions
{
    public interface IUserService
    {
        Task<ServiceResponse<User?>> GetUserByID(int userId);
        Task<ServiceResponse<User>> GetUserByEmail(string email);
        Task<ServiceResponse<List<User>>> GetAllUsers();
        Task<ServiceResponse<int>> CreateUser(UserAdminCreateDTO UserAdminCreateUserDTO);
        Task<ServiceResponse<bool>> updateUser(UserAdminUpdateDTO userAdminUpdateUserDTO, int userID);
        Task<ServiceResponse<bool>> deleteUser(int userId);
    }
}
