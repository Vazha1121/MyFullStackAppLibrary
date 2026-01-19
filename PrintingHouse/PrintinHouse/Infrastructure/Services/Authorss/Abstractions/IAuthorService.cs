using PrintingHouse.Domain.Database.Repositories.DTOs.AuthorDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;

namespace PrintingHouse.Infrastructure.Services.Authorss.Abstractions
{
    public interface IAuthorService
    {
        Task<ServiceResponse<Author>> GetById(int id);
        Task<ServiceResponse<IEnumerable<Author>>> GetPagedAsync(
            string firstName,
            string lastName,
            int pageNumber,
            int pageSize
            );
        Task<ServiceResponse<int>> AddAuthorAsync(CreateAuthorDTO createAuthorDTO);
        Task<ServiceResponse<bool>> Update(UpdateAuthorDTO updateAuthorDTO);
        Task<ServiceResponse<bool>> Delete(int authorId);
    }
}
