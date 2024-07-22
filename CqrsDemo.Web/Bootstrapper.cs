using Fusonic.Extensions.AspNetCore.Http;
using Fusonic.Extensions.Common.Security;
using Fusonic.Extensions.Hangfire;
using SimpleInjector;

namespace CqrsDemoWeb
{
    internal static class Bootstrapper
    {
        public static void RegisterCoreServices(this Container container, ConfigurationManager configuration)
        {
            container.Register<IUserAccessor, HttpContextUserAccessor>(Lifestyle.Singleton);
            container.RegisterOutOfBandDecorators();

            CqrsDemo.Core.Bootstrapper.RegisterCoreServices(container, new CqrsDemo.Core.Bootstrapper.Options()
            {
                UseInMemoryDbContext = false,
                EnableDecorators = true,
                ConnectionString = configuration.GetConnectionString("app")
            });
        }

        public static void GeneratoreSampleData(this Container container)
        {
            CqrsDemo.Core.Bootstrapper.GenerateSampleDataAsync(container).Wait();
        }
    }
}