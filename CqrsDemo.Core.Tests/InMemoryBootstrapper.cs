using CqrsDemo.Core.Domain;
using CqrsDemo.Core.Services.Security;
using CqrsDemo.Infrastructure.Mailing;
using Fusonic.Extensions.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CqrsDemo.Core.Tests
{
    public class InMemoryBootstrapper
    {
        public static Container Bootstrap()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;

            container.Register<SalesContext, SalesContext>();

            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(nameof(SalesContext));
            builder.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            container.RegisterInstance(builder.Options);

            var assemblies = new[] { typeof(SalesContext).Assembly, typeof(InMemoryBootstrapper).Assembly };
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.RegisterSingleton<IMediator, SimpleInjectorMediator>();

            container.RegisterInstance(NSubstitute.Substitute.For<IMailer>());
            container.RegisterInstance(NSubstitute.Substitute.For<IPermissionService<Order>>());
            container.RegisterInstance(NSubstitute.Substitute.For<IOrderNumberGenerator>());
            container.RegisterInstance(NSubstitute.Substitute.For<IOrderCalculator>());

            return container;
        }
    }
}