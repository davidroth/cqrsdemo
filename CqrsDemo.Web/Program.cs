using CqrsDemoWeb;
using Fusonic.Extensions.Hangfire;
using Hangfire;
using Hangfire.SqlServer;
using SimpleInjector;
using SimpleInjector.Lifestyles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLocalization();

var container = new Container();
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
container.Options.DefaultLifestyle = Lifestyle.Scoped;

container.RegisterCoreServices(builder.Configuration);

builder.Services.AddSimpleInjector(container, options =>
{
    options.AddAspNetCore()
        .AddControllerActivation()
        .AddViewComponentActivation()
        .AddPageModelActivation()
        .AddTagHelperActivation();

    options.AddLogging();
    options.AddLocalization();
});

builder.Services.AddHangfire(x =>
{
    x.UseSerializerSettings(new Newtonsoft.Json.JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All });
    x.UseActivator(new ContainerJobActivator(container));
    x.UseSqlServerStorage(() => new Microsoft.Data.SqlClient.SqlConnection(builder.Configuration.GetConnectionString("app")),
       new SqlServerStorageOptions() { SchemaName = "hgf" });

});
builder.Services.AddHangfireServer();

var app = builder.Build();

app.Services.UseSimpleInjector(container);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHangfireDashboard();

app.UseStaticFiles();

Bootstrapper.GeneratoreSampleData(container);

app.Run();