using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintingHouse.Domain.Database.Data;
using PrintingHouse.Domain.Database.Repositories.DTOs.AuthorDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Authorss.Abstractions;

namespace PrintingHouse.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ApplicationDbContext _dbContext;

        public AuthorsController(IAuthorService authorService, ApplicationDbContext dbContext)
        {
            _authorService = authorService;
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        [HttpPost("add-author")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateAuthor(CreateAuthorDTO createAuthorDTO)
        {
            var response = await _authorService.AddAuthorAsync(createAuthorDTO);

            if (response.IsSuccess)
            {
                return Ok(response);    
            }
            else
            {
                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("edit-author")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateAuthor(UpdateAuthorDTO updateAuthorDTO)
        {
            var response = await _authorService.Update(updateAuthorDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("author/{authorID:int}")]
        public async Task<ActionResult<ServiceResponse<Author>>> GetAuthorByID(int authorID)
        {
            var response = await _authorService.GetById(authorID);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetAuthorPaged")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Author>>>> GetAuthorPager(
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _authorService.GetPagedAsync(
            firstName,
            lastName,
            pageNumber,
            pageSize
        );

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("delete-author/{authorID:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteAuthor(int authorId)
        {
            var response = await _authorService.Delete(authorId);

            if(response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        //სერჩისთვის ცალკე რეპოზიტორი და სერვიცი აღარ გავაკეთე

        [HttpGet("searchAuthor")]
        public async Task<ActionResult<ServiceResponse<List<Author>>>> SearchAuthors([FromQuery] string query)
        {
            var response = new ServiceResponse<List<Author>>();

            var author = await _dbContext.Authors.Where(a => a.FirstName.Contains(query) || a.LastName.Contains(query))
                .ToListAsync();

            if(string.IsNullOrWhiteSpace(query))
            {
                

                response.IsSuccess = false;
                response.ErrorMessage = "item wasn't found";
                return BadRequest(response);
            }
            else
            {
                response.IsSuccess = true;
                response.Data = author;
                return Ok(response);
            }
        }

        
    }
}
