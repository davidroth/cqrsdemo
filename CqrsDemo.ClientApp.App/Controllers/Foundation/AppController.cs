namespace CqrsDemo.ClientApp.App.Controllers
{
    public class AppController(IControllerFactory factory)
    {
        private readonly IControllerFactory factory = factory;
        private readonly Dictionary<Controller, IControllerScope<Controller>> controllerScopes = [];

        public async Task<T> RunAsync<T>(Func<T, Task> initAction)
            where T : Controller
        {
            var controllerScope = factory.CreateController<T>();
            var controller = controllerScope.Controller;
            controllerScopes.Add(controller, controllerScope);

            await initAction(controller);
            controller.View.Show();
            return controller;
        }

        public T Run<T>(Action<T> initAction)
            where T : Controller
        {
            var controllerScope = factory.CreateController<T>();
            var controller = controllerScope.Controller;
            controllerScopes.Add(controller, controllerScope);

            initAction(controller);
            controller.View.Show();
            return controller;
        }

        public void Dismiss(Controller controller)
        {
            controller.View.Close();
            if (controllerScopes.Remove(controller, out var value))
            {
                value.Dispose();
            }
        }
    }
}