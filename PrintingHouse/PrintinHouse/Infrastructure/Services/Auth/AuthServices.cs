using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrintingHouse.Domain.Common;
using PrintingHouse.Domain.Database.Data;
using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Database.Repositories.DTOs.UserDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Auth.Abstractions;
using PrintingHouse.Infrastructure.Services.Auth.DTOs;
using PrintingHouse.Infrastructure.Services.EmailSender.Abstractions;
using PrintingHouse.Security;

namespace PrintingHouse.Infrastructure.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        public AuthServices(IUserRepository userRepository, IEmailTemplateRepository emailTemplateRepository, IConfiguration configuration, ApplicationDbContext dbContext,IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _configuration = configuration;
            _dbContext = dbContext;
            _emailSender = emailSender;

        }
        public async Task<ServiceResponse<string>> Login(LoginDTO loginDTO)
        {
            var response = new ServiceResponse<string>();

            var user = await _userRepository.GetUserByEmail(loginDTO.Email);

            if(user == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"User with email - {loginDTO.Email} was not found";


            }
            else if(!VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"User password is incorrect";
            }
            else
            {
                response.Data = GenerateToken(user);
                response.IsSuccess = true;
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(RegisterDTO registerDTO)
        {
            var response = new ServiceResponse<int>();

            var user = await _userRepository.GetUserByEmail(registerDTO.Email);

            if(user == null)
            {
                CreatePasswordHash(registerDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var userToCreate = new CreateUserDTO()
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    Email = registerDTO.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                var createUserId = await _userRepository.CreateUser(userToCreate);

                if (createUserId > 0)
                {
                    await SendVerificationCode(createUserId);
                    response.IsSuccess = true;
                    response.Data = createUserId;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Registration Failed!";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"user with email {registerDTO.Email} is already registered"; 
            }

            return response;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));  
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string  GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsManager ? "Manager" : "User"),
                new Claim(ClaimTypes.Role, user.IsOperator ? "Operator" : "User"),
                new Claim(ClaimTypes.Role, user.IsSeniorOperator ? "Senior Operator" : "User"),
                 new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            
            SecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public async Task<ServiceResponse<bool>> SendVerificationCode(int userId)
        {
            var result = new ServiceResponse<bool>();

            var user = await _userRepository.GetUserByID(userId);

            if(user == null)
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = $"User with ID {userId} was not found";
                return result;
            }
            else
            {
                var code = new Random().Next(100000, 999999).ToString();

                var verificationCode = new VerificationCode
                {
                    Email = user.Email,
                    code = code,
                    ExpirationDate = DateTime.Now.AddMinutes(5),
                    IsUsed = false
                };

                await _dbContext.VerificationCodes.AddAsync(verificationCode);
                await _dbContext.SaveChangesAsync();

                var emailTemplate = await _emailTemplateRepository.GetEmailTemplateByID(EmailTemplates.EmailVerification);

                if (emailTemplate == null)
                {
                    result.IsSuccess = false;
                    result.Data = false;
                    result.ErrorMessage = $"Email template not found.";
                    return result;
                }

                string emailBody = emailTemplate.EmailTemplateBody
                    .Replace("{{USERNAME}}", user.FirstName)
                    .Replace("{{CODE}}", code);

                string emailSubject = emailTemplate.EmailTemplateSubject
                    .Replace("{{USERNAME}}", user.FirstName)
                    .Replace("{{CODE}}", code);

                try
                {
                    await _emailSender.SendEmail(user.Email, emailSubject, emailBody);

                    result.IsSuccess = true;
                    result.Data = true;
                }
                catch (Exception ex)
                {

                    result.IsSuccess = false;
                    result.Data = false;
                    result.ErrorMessage = $"Error sending email: {ex.Message}";
                }

                return result;
            }
        }

        public async Task<ServiceResponse<bool>> VerifyUserWithCode(string email, string code)
        {
            var result = new ServiceResponse<bool>();

            var verificationCode = await _dbContext.VerificationCodes
                .Where(v => v.Email == email && v.code == code && !v.IsUsed)
                .OrderByDescending(v => v.ExpirationDate)
                .FirstOrDefaultAsync();

            if(verificationCode == null)
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = "Verification code is not correct";
            }
            else if(verificationCode.ExpirationDate < DateTime.Now)
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = "Verification code is expired";
            }
            else
            {
                result.IsSuccess = true;
                result.Data = true;
            }

            var user = await _userRepository.GetUserByEmail(email);

            if(user != null && verificationCode != null)
            {
                user.UserIsVerified = true;
                verificationCode.IsUsed = true; 
            }
            return result;
        }
    }
}
