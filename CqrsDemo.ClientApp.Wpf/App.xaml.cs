using System.Reflection;
using System.Windows;
using CqrsDemo.ClientApp.App;
using CqrsDemo.ClientApp.App.Controllers;
using CqrsDemo.Core;

namespace CqrsDemo.ClientApp.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = AppBootstrapper.Bootstrap(Assembly.GetExecutingAssembly(),
                new Bootstrapper.Options()
                {
                    UseInMemoryDbContext = true,
                    EnableDecorators = true,
                    ConfigureLogging = true
                });

            AppBootstrapper.GenerateSampleData(container).Wait();

            var appController = container.GetInstance<AppController>();
            appController.Run<MainController>(x => x.Init());
        }
    }
}