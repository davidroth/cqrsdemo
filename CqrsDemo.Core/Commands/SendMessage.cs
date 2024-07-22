using CqrsDemo.Infrastructure.Mailing;

namespace CqrsDemo.Core.Commands
{
    public class SendMessage : IRequest
    {
        public required string Text { get; set; }
    }

    class SendMessageCommandHandler(IMailer mailer) : IRequestHandler<SendMessage>
    {
        public async Task<Unit> Handle(SendMessage command, CancellationToken cancellationToken)
        {
            await mailer.SendMailAsync("receiver@test.at", command.Text);
            return default;
        }
    }
}