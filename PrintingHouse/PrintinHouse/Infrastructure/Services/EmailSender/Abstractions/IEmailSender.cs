namespace PrintingHouse.Infrastructure.Services.EmailSender.Abstractions
{
    public interface IEmailSender
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}
