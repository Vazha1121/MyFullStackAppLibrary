using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Database.Repositories.DTOs.AuthorDTO;
using PrintingHouse.Domain.Models;
using PrintingHouse.Domain.Models.Shared;
using PrintingHouse.Infrastructure.Services.Authorss.Abstractions;

namespace PrintingHouse.Infrastructure.Services.Authorss
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<ServiceResponse<int>> AddAuthorAsync(CreateAuthorDTO createAuthorDTO)
        {
            var result = new ServiceResponse<int>();

            var authorToCreate = new CreateAuthorDTO()
            {
                FirstName = createAuthorDTO.FirstName,
                LastName = createAuthorDTO.LastName,
                Gender = createAuthorDTO.Gender,
                PersonalNumber = createAuthorDTO.PersonalNumber,
                BirthDate = createAuthorDTO.BirthDate,
                CountryId = createAuthorDTO.CountryId,
                CityId = createAuthorDTO.CityId,
                PhoneNumber = createAuthorDTO.PhoneNumber,
                Email = createAuthorDTO.Email
            };

            var createdAuthor = await _authorRepository.AddAuthorAsync(authorToCreate);

            if(createdAuthor > 0)
            {
                result.IsSuccess = true;
                result.Data = createdAuthor;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Author creation failed";
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Delete(int authorId)
        {
           var result = new ServiceResponse<bool>();

            var deleteAuthor = await _authorRepository.Delete(authorId);

            if (deleteAuthor)
            {
                result.IsSuccess = true;
                result.Data = true;
            }
            else
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = $"Delete of author with ID {authorId} failed";
            }

            return result;
        }

        public async Task<ServiceResponse<Author>> GetById(int id)
        {
            var result = new ServiceResponse<Author>();

            var author = await _authorRepository.GetById(id);

            if(author == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Author with ID {id} wasn't found in database";
            }
            else
            {
                result.IsSuccess = true;
                result.Data = author;
            }

            return result;
        }

        public async Task<ServiceResponse<IEnumerable<Author>>> GetPagedAsync(string firstName, string lastName, int pageNumber, int pageSize)
        {
            var result = new ServiceResponse<IEnumerable<Author>>();
            var authors = await _authorRepository.GetPagedAsync(
                   firstName,
                   lastName,
                   pageNumber,
                   pageSize
               );
            try
            {
                
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "PageNumber და PageSize უნდა იყოს დადებითი რიცხვები";
                    return result;
                }
                else
                {

                    result.IsSuccess = true;
                    result.Data = authors;
                    result.ErrorMessage = "ავტორები წარმატებით ჩაიტვირთა";
                }

                
                
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> Update(UpdateAuthorDTO updateAuthorDTO)
        {
            var result = new ServiceResponse<bool>();

            var authorToUpdate = await _authorRepository.Update(updateAuthorDTO);

            if (authorToUpdate)
            {
                result.IsSuccess = true;
                result.Data = true;
            }
            else
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = $"Update to car with ID {updateAuthorDTO.authorId} failer";
            }
            return result;
        }
    }
}
