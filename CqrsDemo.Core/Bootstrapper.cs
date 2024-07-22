using CqrsDemo.Core.Domain;
using CqrsDemo.Core.Services;
using CqrsDemo.Core.Services.Security;
using CqrsDemo.Infrastructure;
using CqrsDemo.Infrastructure.Caching;
using CqrsDemo.Infrastructure.Mailing;
using Fusonic.Extensions.Common.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Reflection;

namespace CqrsDemo.Core
{
    /// <summary>
    /// Bootstrapper facade to register types of the core project in SimpleInjector to allow re-use in certain kinds of projects (web, rich client, console ...)
    /// <remark>Access to DI Container must only be used in composition root! Never access the Container outside of the composition root as this is a service locator anti pattern!</remark>
    /// </summary>
    public static class Bootstrapper
    {
        public static void RegisterCoreServices(this Container container, Options options)
        {
            var assemblies = new[] { typeof(SalesContext).GetTypeInfo().Assembly };
            
            container.Register<IOrderNumberGenerator, OrderNumberGenerator>(Lifestyle.Scoped);
            container.Register<SecurityContext>(Lifestyle.Scoped);
            container.Register<IPermissionService<Order>, OrderPermissionService>(Lifestyle.Scoped);

            container.Register(typeof(IRequestHandler<,>), assemblies);

            if (options.EnableDecorators)
            {
                container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(LoggingCommandHandlerDecorator<,>));
                container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(RetryCommandHandlerDecorator<,>),
                    x => x.ImplementationType.GetTypeInfo().GetCustomAttribute<RetryCommandAttribute>() != null);

                container.RegisterSingleton<ITransactionScopeHandler, TransactionScopeHandler>();
                container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(TransactionalRequestHandlerDecorator<,>));
                container.RegisterDecorator(typeof(INotificationHandler<>), typeof(TransactionalNotificationHandlerDecorator<>));
                container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(ValidationCommandHandlerDecorator<,>));

                container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(QueryCachingDecorator<,>),
                    x => x.ImplementationType.GetTypeInfo().GetCustomAttribute<CacheAttribute>() != null);
            }

            container.Collection.Register(typeof(INotificationHandler<>), assemblies);

            container.Register<SalesContext, SalesContext>(Lifestyle.Scoped);
            container.RegisterSalesContext(options);

            container.Register<IOrderCalculator, OrderCalculator>();
            container.Register<IMailer, ConsoleMailer>();
            container.Register<ICache, InMemoryCache>(Lifestyle.Singleton);

            container.Register<SampleDataGenerator>();
            container.RegisterSingleton<IMediator, SimpleInjectorMediator>();

            if (options.ConfigureLogging)
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

                container.RegisterInstance(loggerFactory);
                container.Register(typeof(ILogger<>), typeof(Logger<>));
            }
        }

        private static void RegisterSalesContext(this Container container, Options options)
        {
            var builder = new DbContextOptionsBuilder();
            if (options.UseInMemoryDbContext)
            {
                builder.UseInMemoryDatabase(nameof(SalesContext));
                builder.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            }
            else if (!string.IsNullOrEmpty(options.ConnectionString))
            {
                builder.UseSqlServer(options.ConnectionString);
            }
            else throw new InvalidOperationException("Cannot configure for sql server because options.ConnectionString is null or empty");

            container.RegisterInstance(builder.Options);
        }

        public class Options
        {
            public bool UseInMemoryDbContext { get; set; }
            public string? ConnectionString { get; set; }
            public bool EnableDecorators { get; set; }
            public bool ConfigureLogging { get; set; }
        }

        public static async Task GenerateSampleDataAsync(Container container)
        {
            var syncContext = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(null);
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var generator = container.GetInstance<SampleDataGenerator>();
                await generator.GenerateSampleData();
            }
            SynchronizationContext.SetSynchronizationContext(syncContext);
        }
    }
}