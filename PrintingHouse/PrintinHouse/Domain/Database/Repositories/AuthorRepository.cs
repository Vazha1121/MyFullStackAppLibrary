using Microsoft.EntityFrameworkCore;
using PrintingHouse.Domain.Database.Data;
using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Database.Repositories.DTOs.AuthorDTO;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddAuthorAsync(CreateAuthorDTO createAuthorDTO)
        {
            var result = default(int);

            var author = new Author()
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

            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            result = author.authorId;

            return result;
        }

        public async Task<bool> Delete(int authorId)
        {
            var result = default(bool);

            var author = await _dbContext.Authors.FirstOrDefaultAsync(a => a.authorId == authorId);

            if(author == null)
            {
                result = false;
            }
            else
            {
               _dbContext.Authors.Remove(author);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<Author> GetById(int id)
        {
            var result = new Author();

            result = await _dbContext.Authors.FirstOrDefaultAsync(a => a.authorId == id);

            return result;
        }

        public async Task<IEnumerable<Author>> GetPagedAsync(string firstName, string lastName, int pageNumber, int pageSize)
        {
            var query = _dbContext.Authors.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(a => a.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(a => a.LastName.Contains(lastName));
            }

            
            return await query
                .OrderBy(a => a.authorId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> Update(UpdateAuthorDTO updateAuthorDTO)
        {
            var result = default(bool);

            var authorToUpdate = await _dbContext.Authors.FirstOrDefaultAsync(a => a.authorId == updateAuthorDTO.authorId);

            if (authorToUpdate == null)
            {
                result = false;
            }
            else
            {
                authorToUpdate.FirstName = updateAuthorDTO.FirstName;
                authorToUpdate.LastName = updateAuthorDTO.LastName;
                authorToUpdate.Gender = updateAuthorDTO.Gender;
                authorToUpdate.PersonalNumber = updateAuthorDTO.PersonalNumber;
                authorToUpdate.BirthDate = updateAuthorDTO.BirthDate;
                authorToUpdate.Country = updateAuthorDTO.Country;
                authorToUpdate.City = updateAuthorDTO.City;
                authorToUpdate.PhoneNumber = updateAuthorDTO.PhoneNumber;
                authorToUpdate.Email = updateAuthorDTO.Email;

                result = true;

                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
