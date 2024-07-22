using CqrsDemo.ClientApp.App.Infrastructure;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public class ControllerContext(AppController controller, LifetimeScopedMediator mediator)
    {
        public LifetimeScopedMediator Mediator { get; } = mediator;
        public AppController AppController { get; } = controller;
    }
}