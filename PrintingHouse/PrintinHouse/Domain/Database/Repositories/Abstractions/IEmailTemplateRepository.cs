using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories.Abstractions
{
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplate> GetEmailTemplateByID(int emailTemplateId);
    }
}
