namespace CqrsDemo.Infrastructure.Mailing
{
    public interface IMailer
    {
        Task SendMailAsync(string receiver, string message);
    }
}