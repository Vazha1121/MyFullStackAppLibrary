using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Auth.DTOs;

namespace PrintingHouse.Infrastructure.Services.Auth.Abstractions
{
    public interface IAuthServices
    {
        public Task<ServiceResponse<int>> Register(RegisterDTO registerDTO);
        public Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
        public Task<ServiceResponse<bool>> SendVerificationCode(int userId);
        public Task<ServiceResponse<bool>> VerifyUserWithCode(string email, string code);
    }
}
