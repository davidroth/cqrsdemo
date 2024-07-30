using Fusonic.Extensions.Mediator;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CqrsDemo.ClientApp.App.Infrastructure
{
    public sealed class LifetimeScopedMediator(Container container, SimpleInjectorMediator mediator) : IMediator
    {
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                return await mediator.Send(request, cancellationToken);
            }
        }

        public async Task Send<TRequest>(IRequest request, CancellationToken cancellationToken = default)
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                await mediator.Send(request, cancellationToken);
            }
        }

        public async Task Publish(INotification notification, CancellationToken cancellationToken = default)
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                await mediator.Publish(notification, cancellationToken);
            }
        }

        public IAsyncEnumerable<TResponse> CreateAsyncEnumerable<TResponse>(IAsyncEnumerableRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException("Not supported");
        }
    }
}