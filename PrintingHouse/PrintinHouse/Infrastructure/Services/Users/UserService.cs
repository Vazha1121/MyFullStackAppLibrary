using System.Security.Cryptography;
using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Database.Repositories.DTOs.UserDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Users.Abstractions;
using PrintingHouse.Infrastructure.Services.Users.DTOs;

namespace PrintingHouse.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }
        public async Task<ServiceResponse<int>> CreateUser(UserAdminCreateDTO userAdminCreateUserDTO)
        {
            var result = new ServiceResponse<int>();

            CreatePasswordHash(userAdminCreateUserDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userToCreate = new CreateUserDTO()
            {
                FirstName = userAdminCreateUserDTO.FirstName,
                LastName = userAdminCreateUserDTO.LastName,
                Email = userAdminCreateUserDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
                
            };

            var createdUser = await _userRepository.CreateUser(userToCreate);

            if(createdUser > 0)
            {
                result.IsSuccess = true;
                result.Data = createdUser;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "User Creation failed";
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> deleteUser(int userId)
        {
           var result = new ServiceResponse<bool>();

           var deletedUser = await _userRepository.deleteUser(userId);

            if (deletedUser)
            {
                result.IsSuccess = true;    
                result.Data = deletedUser;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Deleting user with ID {userId} has failed";
            }

            return result;
        }

        public async Task<ServiceResponse<List<User>>> GetAllUsers()
        {
            var result = new ServiceResponse<List<User>>();

            var users = await _userRepository.GetAllUsers();

            if (users.Any())
            {
                result.IsSuccess = true;
                result.Data = users;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "No user in database";
            }

            return result;
        }

        public async Task<ServiceResponse<User>> GetUserByEmail(string email)
        {
            var result = new ServiceResponse<User>();

            var userEmail = await _userRepository.GetUserByEmail(email);

            if(userEmail != null)
            {
                result.IsSuccess = true;
                result.Data = userEmail;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"User with Email {userEmail} was not found";
            }
            return result;
        }

        public async Task<ServiceResponse<User?>> GetUserByID(int userId)
        {
            var result = new ServiceResponse<User?>();

            var user = await _userRepository.GetUserByID(userId);

            if (user == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"User with ID {userId} was not found";

            }
            else
            {
                result.IsSuccess = true;
                result.Data = user;
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> updateUser(UserAdminUpdateDTO userAdminUpdateDTO, int userID)
        {
            var result = new ServiceResponse<bool>();

            CreatePasswordHash(userAdminUpdateDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var usetToUpdate = new UpdateUserDTO()
            {
                FirstName = userAdminUpdateDTO.FirstName,
                LastName = userAdminUpdateDTO.LastName,
                Email = userAdminUpdateDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsAdmin = userAdminUpdateDTO.IsAdmin,

            };

            var update= await _userRepository.updateUser(usetToUpdate, userID);

            return result;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
