using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace konatus.data.Context
{
    public class KonatusContextFactory : IDesignTimeDbContextFactory<KonatusDbContext>
    {
        KonatusDbContext IDesignTimeDbContextFactory<KonatusDbContext>.CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                //            .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<KonatusDbContext>();
            optionsBuilder.UseNpgsql(Environment.ExpandEnvironmentVariables(configuration.GetConnectionString("Postgresql"))
                       .Replace("%POSTGRES_HOST%", "localhost"),
                               p => p.EnableRetryOnFailure(
                               maxRetryCount: 6,
                               maxRetryDelay: TimeSpan.FromSeconds(5),
                               errorCodesToAdd: null));

            return new KonatusDbContext(optionsBuilder.Options);
        }
    }
}