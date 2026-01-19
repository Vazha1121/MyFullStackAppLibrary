using Microsoft.EntityFrameworkCore;
using PrintingHouse.Domain.Database.Data;
using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Database.Repositories.DTOs.UserDTO;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateUser(CreateUserDTO createUserDTO)
        {
            var result = default(int);
            var user = new User()
            {
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                Email = createUserDTO.Email,
                PasswordHash = createUserDTO.PasswordHash,
                PasswordSalt = createUserDTO.PasswordSalt,
            };
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            result = user.UserId;

            return result;
        }

        public async Task<bool> deleteUser(int userId)
        {
            var result = default(bool);
            var user = await _dbContext.Users.FirstOrDefaultAsync(o => o.UserId == userId);
            if(user == null)
            {
                result = false;
            }
            else
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var result = new List<User>();
            result = await _dbContext.Users.ToListAsync();
            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = new User();
            result = await _dbContext.Users.FirstOrDefaultAsync(o => o.Email == email);
            return result;
        }

        public async Task<User> GetUserByID(int userId)
        {
            var result = new User();
            result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return result;
        }

        public async Task<bool> updateUser(UpdateUserDTO updateUserDTO, int userID)
        {
           var result = default(bool);

           var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(o => o.UserId == userID);
           if(userToUpdate == null)
           {
                result = false;
           }
            else
            {
                userToUpdate.FirstName = updateUserDTO.FirstName;
                userToUpdate.LastName = updateUserDTO.LastName;
                userToUpdate.Email = updateUserDTO.Email;
                userToUpdate.PasswordHash = updateUserDTO.PasswordHash;
                userToUpdate.PasswordSalt = updateUserDTO.PasswordSalt;
                userToUpdate.IsAdmin = updateUserDTO.IsAdmin;

                result = true;

                await _dbContext.SaveChangesAsync();
            }
           return result;
        }
    }
}
