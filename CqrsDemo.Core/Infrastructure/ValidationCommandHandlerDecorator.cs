using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Infrastructure
{
    public class ValidationCommandHandlerDecorator<TCommand, TResult> : IRequestHandler<TCommand, TResult> 
        where TCommand : IRequest<TResult>
    {
        private readonly IRequestHandler<TCommand, TResult> decoratee;
        public ValidationCommandHandlerDecorator(IRequestHandler<TCommand, TResult> decoratee) => this.decoratee = decoratee;

        public Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            Validator.ValidateObject(command, new ValidationContext(command));
            return decoratee.Handle(command, cancellationToken);
        }
    }
}