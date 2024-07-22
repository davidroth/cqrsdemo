using CqrsDemo.ClientApp.App.Controllers;
using CqrsDemo.ClientApp.App.Infrastructure;
using CqrsDemo.Core;
using Fusonic.Extensions.Mediator;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Reflection;

namespace CqrsDemo.ClientApp.App
{
    public static class AppBootstrapper
    {
        public static Container Bootstrap(Assembly presentationAssembly, Bootstrapper.Options options)
        {
            var container = new Container();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;
            container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(
                defaultLifestyle: new AsyncScopedLifestyle(),
                fallbackLifestyle: ScopedLifestyle.Flowing);

            container.RegisterCoreServices(options);

            foreach (var controller in container.GetTypesToRegister<IController>(typeof(MainController).Assembly))
            {
                container.Register(controller);
            }

            // This is the only singleton controller => AppController is the root controller of the application and coordinates the start/stop of other controllers
            container.RegisterSingleton<AppController>();

            foreach (var view in from type in presentationAssembly.GetExportedTypes()
                                 where typeof(IView).IsAssignableFrom(type)
                                 let interfaces = type.GetInterfaces().Where(x => typeof(IView).IsAssignableFrom(x) && x != typeof(IView))
                                 select new { Service = interfaces.Single(), Implementation = type })
            {
                container.Register(view.Service, view.Implementation, Lifestyle.Scoped);
            }
                        
            container.RegisterSingleton<SimpleInjectorMediator>();
            container.RegisterSingleton<LifetimeScopedMediator>();
            container.RegisterSingleton<ControllerContext>();
            container.RegisterInstance<IControllerFactory>(new ControllerFactory(container));
            
            container.Verify();
            return container;
        }

        public static Task GenerateSampleData(Container container) => Bootstrapper.GenerateSampleDataAsync(container);

        private sealed class ControllerFactory(Container container) : IControllerFactory
        {
            public ControllerScope<TController> CreateController<TController>() where TController : Controller
            {
                var scope = new Scope(container);
                var scopedController = new ControllerScope<TController>(scope.GetInstance<TController>(), scope);
                return scopedController;
            }
        }
    }
}