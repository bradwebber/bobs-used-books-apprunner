using Amazon.Extensions.Configuration.SystemsManager;
using Amazon.Extensions.NETCore.Setup;
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
                Console.WriteLine("AddSystemsManager(builder.Configuration, \"/BobsBookstore/\");");
                AddSystemsManager(builder.Configuration, "/BobsBookstore/");

                Console.WriteLine($"Connection String: { builder.Configuration.GetConnectionString("BookstoreDbDefaultConnection") }");

                Console.WriteLine("return builder");
                return builder;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());

                throw;
            }            
        }

        public static IConfigurationBuilder AddSystemsManager(IConfigurationBuilder builder, string path)
        {
            Console.WriteLine("Entered AddSystemsManager(IConfigurationBuilder builder, string path)");

            Console.WriteLine("if (path == null)");
            if (path == null)
            {
                Console.WriteLine("throw new ArgumentNullException(\"path\");");
                throw new ArgumentNullException("path");
            }

            Console.WriteLine("return AddSystemsManager(builder, ConfigureSource(path, null));");
            return AddSystemsManager(builder, ConfigureSource(path, null));
        }

        public static IConfigurationBuilder AddSystemsManager(IConfigurationBuilder builder, Action<SystemsManagerConfigurationSource> configureSource)
        {
            Console.WriteLine("Entered AddSystemsManager(IConfigurationBuilder builder, Action<SystemsManagerConfigurationSource> configureSource)");

            Console.WriteLine("if (configureSource == null)");
            if (configureSource == null)
            {
                Console.WriteLine("throw new ArgumentNullException(\"configureSource\");");
                throw new ArgumentNullException("configureSource");
            }

            Console.WriteLine("SystemsManagerConfigurationSource systemsManagerConfigurationSource = new SystemsManagerConfigurationSource()");
            SystemsManagerConfigurationSource systemsManagerConfigurationSource = new SystemsManagerConfigurationSource();


            Console.WriteLine("configureSource(systemsManagerConfigurationSource);");
            configureSource(systemsManagerConfigurationSource);


            Console.WriteLine("if (string.IsNullOrWhiteSpace(systemsManagerConfigurationSource.Path))");
            if (string.IsNullOrWhiteSpace(systemsManagerConfigurationSource.Path))
            {
                Console.WriteLine("throw new ArgumentNullException(\"Path\");");
                throw new ArgumentNullException("Path");
            }

            Console.WriteLine("if (systemsManagerConfigurationSource.AwsOptions != null)");
            if (systemsManagerConfigurationSource.AwsOptions != null)
            {
                Console.WriteLine("return builder.Add(systemsManagerConfigurationSource);");
                return builder.Add(systemsManagerConfigurationSource);
            }

            Console.WriteLine("systemsManagerConfigurationSource.AwsOptions = GetAwsOptions(builder);");
            systemsManagerConfigurationSource.AwsOptions = GetAwsOptions(builder);

            Console.WriteLine("return builder.Add(systemsManagerConfigurationSource);");
            return builder.Add(systemsManagerConfigurationSource);
        }

        private static Action<SystemsManagerConfigurationSource> ConfigureSource(string path, AWSOptions awsOptions, bool optional = false, TimeSpan? reloadAfter = null)
        {
            Console.WriteLine("Entered ConfigureSource(string path, AWSOptions awsOptions, bool optional = false, TimeSpan? reloadAfter = null)");

            Console.WriteLine("return delegate (SystemsManagerConfigurationSource configurationSource)");
            return delegate (SystemsManagerConfigurationSource configurationSource)
            {
                configurationSource.Path = path;
                configurationSource.AwsOptions = awsOptions;
                configurationSource.Optional = optional;
                configurationSource.ReloadAfter = reloadAfter;
            };
        }

        private static AWSOptions GetAwsOptions(IConfigurationBuilder builder)
        {
            Console.WriteLine("Entered GetAwsOptions(IConfigurationBuilder builder)");

            Console.WriteLine("if (builder.Properties.TryGetValue(\"AWS_CONFIGBUILDER_AWSOPTIONS\", out var value))");
            if (builder.Properties.TryGetValue("AWS_CONFIGBUILDER_AWSOPTIONS", out var value))
            {
                Console.WriteLine("AWSOptions aWSOptions = value as AWSOptions;");
                AWSOptions aWSOptions = value as AWSOptions;

                Console.WriteLine("if (aWSOptions != null)");
                if (aWSOptions != null)
                {
                    Console.WriteLine("return aWSOptions;");
                    return aWSOptions;
                }
            }

            Console.WriteLine("AWSOptions aWSOptions2 = builder.Build().GetAWSOptions();");
            AWSOptions aWSOptions2 = builder.Build().GetAWSOptions();

            Console.WriteLine("if (builder.Properties.ContainsKey(\"AWS_CONFIGBUILDER_AWSOPTIONS\"))");
            if (builder.Properties.ContainsKey("AWS_CONFIGBUILDER_AWSOPTIONS"))
            {
                Console.WriteLine("builder.Properties[\"AWS_CONFIGBUILDER_AWSOPTIONS\"] = aWSOptions2;");
                builder.Properties["AWS_CONFIGBUILDER_AWSOPTIONS"] = aWSOptions2;
            }
            else
            {
                Console.WriteLine("else");

                Console.WriteLine("builder.Properties.Add(\"AWS_CONFIGBUILDER_AWSOPTIONS\", aWSOptions2);");
                builder.Properties.Add("AWS_CONFIGBUILDER_AWSOPTIONS", aWSOptions2);
            }

            Console.WriteLine("return aWSOptions2;");
            return aWSOptions2;
        }
    }
}