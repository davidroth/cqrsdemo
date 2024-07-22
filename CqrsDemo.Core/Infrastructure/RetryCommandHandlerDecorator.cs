namespace CqrsDemo.Infrastructure
{
    public class RetryCommandHandlerDecorator<TCommand, TResult> : IRequestHandler<TCommand, TResult> 
        where TCommand : IRequest<TResult>
    {
        private readonly IRequestHandler<TCommand, TResult> decorated;

        public RetryCommandHandlerDecorator(IRequestHandler<TCommand, TResult> decorated)
        {
            this.decorated = decorated;
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await HandleWithCountDown(request, 5, cancellationToken);
        }

        private async Task<TResult> HandleWithCountDown(TCommand command, int count, CancellationToken cancellationToken)
        {
            try
            {
                return await decorated.Handle(command, cancellationToken);
            }
            catch (Exception ex)
            {
                if (count <= 0 || !IsRetryable(ex))
                    throw;

                Thread.Sleep(300);
                return await HandleWithCountDown(command, count - 1, cancellationToken);
            }
        }

        private static bool IsRetryable(Exception? ex)
        {
            while (ex != null)
            {
                if (ex is InvalidOperationException && ex.Message.Contains("timeout"))
                    return true;

                ex = ex.InnerException;
            }

            return false;
        }
    }
}