namespace CqrsDemo.Infrastructure.Mailing
{
    public class ConsoleMailer : IMailer
    {
        public Task SendMailAsync(string receiver, string message)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Sending mail. Receiver: {receiver} Text: {message}");
            return Task.CompletedTask;
        }
    }
}