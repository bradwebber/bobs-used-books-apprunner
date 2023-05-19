using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Bookstore.Web.Startup;
using System;
using System.IO;

var options = new WebApplicationOptions() { Args = args, ContentRootPath = AppContext.BaseDirectory, WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot") };
var builder = WebApplication.CreateBuilder(options);

Console.WriteLine($"Content Root Path: {options.ContentRootPath}");
Console.WriteLine($"Web Root Path: {options.WebRootPath}");
Console.WriteLine($"Environment: {options.EnvironmentName}");

try
{
    Console.WriteLine("Configuring configuration...");
    builder.ConfigureConfiguration();
    Console.WriteLine("Configuration configured.");

    Console.WriteLine("Configuring services...");
    builder.ConfigureServices();
    Console.WriteLine("Services configured.");

    Console.WriteLine("Configuring authentication...");
    builder.ConfigureAuthentication();
    Console.WriteLine("Authentication configured.");

    Console.WriteLine("Configuring dependency injection...");
    builder.ConfigureDependencyInjection();
    Console.WriteLine("Dependency injection configured.");

    var app = builder.Build();

    Console.WriteLine("Configuring middleware...");
    await app.ConfigureMiddlewareAsync();
    Console.WriteLine("Middleware configured.");

    app.Run();
}
catch (Exception ex)
{
    Console.Write(ex.ToString());
	throw;
}
