using Fusonic.Extensions.Mediator;

namespace CqrsDemo.ClientApp.App.Controllers
{
    static class ControllerExtensions
    {
        public static Task<TResult> QueryAsync<TResult>(this Controller controller, IRequest<TResult> query)
            where TResult : class
        {
            return controller.Context.Mediator.Send(query, default);
        }

        public static Task ExecuteAsync<TCommand>(this Controller controller, TCommand command)
           where TCommand : ICommand
        {
            return controller.Context.Mediator.Send(command, default);
        }
    }
}