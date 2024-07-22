using Microsoft.Extensions.Logging;

namespace CqrsDemo.Infrastructure
{
    public class LoggingCommandHandlerDecorator<TCommand, TResult> : IRequestHandler<TCommand, TResult>  where TCommand : IRequest<TResult>
    {
        private readonly IRequestHandler<TCommand, TResult> decoratee;
        private readonly ILogger<TCommand> logger;

        public LoggingCommandHandlerDecorator(IRequestHandler<TCommand, TResult> decoratee, ILogger<TCommand> logger)
        {
            this.decoratee = decoratee;
            this.logger = logger;
        }

        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Executing command: {command}", command);
                return await decoratee.Handle(command, cancellationToken);
            }
            finally
            {
                logger.LogInformation("Executed command: {command}", command);
            }
        }
    }
}