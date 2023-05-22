using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Bookstore.Web.Startup;
using System;
using System.IO;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();

try
{
    var options = new WebApplicationOptions() { Args = args, ContentRootPath = AppContext.BaseDirectory, WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot") };
    var builder = WebApplication.CreateBuilder(options);

    builder.Host.UseSerilog();

    builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

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
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}