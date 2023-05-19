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
    builder.ConfigureConfiguration();

    builder.ConfigureServices();

    builder.ConfigureAuthentication();

    builder.ConfigureDependencyInjection();

    var app = builder.Build();

    await app.ConfigureMiddlewareAsync();

    app.Run();
}
catch (Exception ex)
{
    Console.Write(ex.ToString());
	throw;
}
