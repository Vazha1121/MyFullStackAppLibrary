using PrintingHouse.Domain.Database.Repositories.DTOs.AuthorDTO;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories.Abstractions
{
    public interface IAuthorRepository
    {
        Task<Author> GetById(int id);
        Task<IEnumerable<Author>> GetPagedAsync(
            string firstName,
            string lastName,
            int pageNumber,
            int pageSize
            );
        Task<int> AddAuthorAsync(CreateAuthorDTO createAuthorDTO);
        Task<bool> Update(UpdateAuthorDTO updateAuthorDTO);
        Task<bool> Delete(int authorId);
    }
}
