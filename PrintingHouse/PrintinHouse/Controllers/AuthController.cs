using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Auth.Abstractions;
using PrintingHouse.Infrastructure.Services.Auth.DTOs;

namespace PrintingHouse.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterDTO registerDTO)
        {
            var result = await _authServices.Register(registerDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result.ErrorMessage);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDTO loginDTO)
        {
            var response = await _authServices.Login(loginDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.ErrorMessage);
            }
        }

        [HttpPost("send-code")]
        public async Task<ActionResult> SendCode(int userId)
        {
            await _authServices.SendVerificationCode(userId);
            return Ok();
        }

        [HttpPost("verify-code")]
        public async Task<ActionResult<ServiceResponse<bool>>> VerifyCode(string email, string code)
        {
            var response = await _authServices.VerifyUserWithCode(email, code);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
