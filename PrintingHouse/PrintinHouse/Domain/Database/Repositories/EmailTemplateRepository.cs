using Microsoft.EntityFrameworkCore;
using PrintingHouse.Domain.Database.Data;
using PrintingHouse.Domain.Database.Repositories.Abstractions;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmailTemplateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmailTemplate> GetEmailTemplateByID(int emailTemplateId)
        {
            var result = new EmailTemplate();

            result = await _dbContext.EmailTemplates.FirstOrDefaultAsync(o => o.EmailTemplateID == emailTemplateId);

            return result;
        }
    }
}
