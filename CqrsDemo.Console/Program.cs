using CqrsDemo.Core;
using SimpleInjector.Lifestyles;
using SimpleInjector;
using Fusonic.Extensions.Mediator;
using CqrsDemo.Core.Commands;

var container = new Container();
container.Options.DefaultLifestyle = Lifestyle.Scoped;
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

container.RegisterCoreServices(new Bootstrapper.Options()
{
    EnableDecorators = true,
    UseInMemoryDbContext = true,
    ConfigureLogging = true
});

using (AsyncScopedLifestyle.BeginScope(container))
{
    var generator = container.GetInstance<SampleDataGenerator>();
    await generator.GenerateSampleData();
}

using (AsyncScopedLifestyle.BeginScope(container))
{
    await container.GetInstance<IMediator>().Send(new CancelOrder() { OrderId = 1, Reason = "Insufficient funds" });
}

Console.WriteLine("...");
