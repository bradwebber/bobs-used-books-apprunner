using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Bookstore.Web.Startup;
using System;

var options = new WebApplicationOptions() { Args = args, ContentRootPath = AppContext.BaseDirectory });
var builder = WebApplication.CreateBuilder(options);

Console.WriteLine($"Content Root Path: {options.ContentRootPath}");
Console.WriteLine($"Web Root Path: {options.WebRootPath}");
Console.WriteLine($"Environment: {options.EnvironmentName}");

builder.ConfigureConfiguration();

builder.ConfigureServices();

builder.ConfigureAuthentication();

builder.ConfigureDependencyInjection();

var app = builder.Build();

await app.ConfigureMiddlewareAsync();

app.Run();