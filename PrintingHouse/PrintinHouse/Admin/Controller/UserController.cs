using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Users.Abstractions;
using PrintingHouse.Infrastructure.Services.Users.DTOs;

namespace PrintingHouse.Admin.Controller
{
    
    [Route("api/admin/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("user/{userID:int}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int userID)
        {
            var response = await _userService.GetUserByID(userID);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetAllUser()
        {
            var response = await _userService.GetAllUsers();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-user-email")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUserByEmail(string email)
        {
            var response = await _userService.GetUserByEmail(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-user")]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateUser(UserAdminCreateDTO userAdminCreateDTO)
        {
            var response = await _userService.CreateUser(userAdminCreateDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-user/{userID:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser(UserAdminUpdateDTO userAdminUpdateDTO, int userID)
        {
            var response = await _userService.updateUser(userAdminUpdateDTO, userID);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{userID:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser(int userID)
        {
            var response = await _userService.deleteUser(userID);

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
