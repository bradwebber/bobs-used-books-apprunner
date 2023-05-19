using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Bookstore.Web.Startup
{
    public static class ConfigurationSetup
    {
        public static WebApplicationBuilder ConfigureConfiguration(this WebApplicationBuilder builder)
        {
            try
            {
                builder.Configuration.AddSystemsManager("/BobsBookstore/");

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