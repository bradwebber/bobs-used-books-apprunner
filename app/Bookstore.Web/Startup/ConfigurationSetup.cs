using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using Bookstore.Domain.AdminUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System;

namespace Bookstore.Web.Startup
{
    public static class ConfigurationSetup
    {
        public static WebApplicationBuilder ConfigureConfiguration(this WebApplicationBuilder builder)
        {
            if (!builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddSystemsManager("/BobsBookstore/");
            }

            if (builder.Environment.IsProduction()) BuildConnectionString(builder);

            return builder;
        }

        private static void BuildConnectionString(WebApplicationBuilder builder)
        {
            // This is the key of a string value in Parameter Store containing the name of the
            // secret in Secrets Manager that in turn contains the credentials of the database in
            // Amazon RDS. The reason for the indirection is that a secret name is suffixed automatically
            // by the CDK with a random string. Using a fixed Parameter Store value to point to the
            // randomly-named secret insulates the application from variability in the name of
            // the secret.
            const string DbSecretsParameterName = "dbsecretsname";

            try
            {
                var dbSecretId = builder.Configuration[DbSecretsParameterName];

                Console.WriteLine($"Reading db credentials from secret {dbSecretId}");

                // Read the db secrets posted into Secrets Manager by the CDK. The secret provides the host,
                // port, userid, and password, which we format into the final connection string for SQL Server.
                // For this code to work locally, appsettings.json must contain an AWS object with profile and
                // region info. When deployed to an EC2 instance, credentials and region will be inferred from
                // the instance profile applied to the instance.
                IAmazonSecretsManager secretsManagerClient;

                var options = builder.Configuration.GetAWSOptions();
                if (options != null)
                {
                    // local "integrated" debug mode using credentials/region in appsettings
                    secretsManagerClient = options.CreateServiceClient<IAmazonSecretsManager>();
                }
                else
                {
                    // deployed mode using credentials/region inferred on host
                    secretsManagerClient = new AmazonSecretsManagerClient();
                }

                var response = secretsManagerClient.GetSecretValueAsync(new GetSecretValueRequest
                {
                    SecretId = dbSecretId
                }).Result;

                var dbSecrets = JsonSerializer.Deserialize<DbSecrets>(response.SecretString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var partialConnString = $"Server={dbSecrets.Host},{dbSecrets.Port}; Initial Catalog=BobsUsedBookStore; MultipleActiveResultSets=true; Integrated Security=false;";

                var connectionBuilder = new SqlConnectionStringBuilder(partialConnString)
                {
                    UserID = dbSecrets.Username,
                    Password = dbSecrets.Password
                };

                builder.Configuration["BookstoreDbDefaultConnection"] = connectionBuilder.ConnectionString;
            }
            catch (AmazonSecretsManagerException e)
            {
                Console.WriteLine($"Failed to read secret {builder.Configuration[DbSecretsParameterName]}, error {e.Message}, inner {e.InnerException.Message}");
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Failed to parse content for secret {builder.Configuration[DbSecretsParameterName]}, error {e.Message}");
            }
        }
    }
}