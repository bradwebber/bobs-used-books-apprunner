using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Bookstore.Web.Startup;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseContentRoot(AppContext.BaseDirectory);

builder.ConfigureConfiguration();

builder.ConfigureServices();

builder.ConfigureAuthentication();

builder.ConfigureDependencyInjection();

var app = builder.Build();

await app.ConfigureMiddlewareAsync();

app.Run();