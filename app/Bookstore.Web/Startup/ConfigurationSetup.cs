using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bookstore.Web.Startup
{
    public static class ConfigurationSetup
    {
        public static WebApplicationBuilder ConfigureConfiguration(this WebApplicationBuilder builder)
        {
            try
            {
                Console.WriteLine("Before AddSystemsManager");
                builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
                builder.Configuration.AddSystemsManager("/BobsBookstore/");
                Console.WriteLine("After AddSystemsManager");

                return builder;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());

                throw;
            }            
        }
    }
}