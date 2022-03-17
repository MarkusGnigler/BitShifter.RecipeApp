//#define MS_SERVER
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BitShifter.Shared.Abstractions.Interfaces;
using BitShifter.Shared.Infrastructure.Services;

namespace BitShifter.Shared.Infrastructure.EfCore
{
    //https://docs.microsoft.com/de-de/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    public abstract class EfCoreContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            var connectionString = GeConnectionString();

            IDateTime dateTimeService = new DateTimeService();
#if MS_SERVER
            optionsBuilder.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly(typeof(TContext).Assembly.FullName));
#else
            optionsBuilder.UseNpgsql(connectionString);
#endif
            //optionsBuilder.UseSqlite("Data source=../PixelDance.Infrastructure/Persistence/Database/recipes.db");

            return Activator.CreateInstance(typeof(TContext), new object[] { optionsBuilder.Options })
                as TContext;
        }

        protected string GeConnectionString()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var appsettings = string.IsNullOrEmpty(environment)
                ? $"appsettings.json"
                : $"appsettings.{environment}.json";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appsettings)
                .Build();

            //var connectionString = configuration.GetConnectionString("SqlServerContext");
            var connectionString = configuration.GetValue<string>("EfCore:ConnectionString");

            Console.WriteLine();
            Console.WriteLine($"{this.GetType().Name}Factory connectionString \"{connectionString}\"");
            Console.WriteLine();

            return connectionString;
        }
    }
}
