using SimpleInjector;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public interface IControllerFactory
    {
        ControllerScope<TController> CreateController<TController>() where TController : Controller;
    }

    public class ControllerScope<TController>(TController controller, Scope scope) : IControllerScope<TController>
    {
        public TController Controller => controller;

        public void Dispose()
        {
            scope.Dispose();
        }
    }

    public interface IControllerScope<out TController> : IDisposable
    {
        TController Controller { get; }
    }
}