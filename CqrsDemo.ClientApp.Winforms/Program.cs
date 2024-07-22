using CqrsDemo.ClientApp.App;
using CqrsDemo.ClientApp.App.Controllers;
using CqrsDemo.Core;
using SimpleInjector;
using System.Reflection;

namespace CqrsDemo.ClientApp.Winforms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);

            var container = AppBootstrapper.Bootstrap(Assembly.GetExecutingAssembly(),
                new Bootstrapper.Options()
                {
                    UseInMemoryDbContext = true,
                    EnableDecorators = true,
                    ConfigureLogging = true
                });

            await AppBootstrapper.GenerateSampleData(container);

            var appController = container.GetInstance<AppController>();

            using var scope = new Scope(container);
            var mainCtrl = scope.GetInstance<MainController>();
            mainCtrl.Init();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run((Form)mainCtrl.View);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}