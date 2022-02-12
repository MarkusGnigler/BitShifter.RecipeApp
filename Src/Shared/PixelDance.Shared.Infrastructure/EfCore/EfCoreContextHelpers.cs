//#define MS_SERVER
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Shared.Kernel.Interfaces;
using PixelDance.Shared.Infrastructure.Utilities;
using PixelDance.Shared.Infrastructure.EfCore.Repository;
using PixelDance.Shared.Abstractions.EfCore.Repository;

namespace PixelDance.Shared.Infrastructure.EfCore
{
    public static class EfCoreContextHelpers
    {
        public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services) 
            where TContext : DbContext
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseInMemoryDatabase("CleanArchitectureDb"));
            //}
            //else
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.usesqlserver(
            //            configuration.getconnectionstring("defaultconnection"),
            //            b => b.migrationsassembly(typeof(applicationdbContext).Assembly.FullName)));
            //}

            var options = services.GetOptions<EfCoreOptions>("EfCore");
            var connectionString = options.ConnectionString;

            if (options.UseInMemoryDatabase)
            {
                services.AddDbContext<TContext>(options =>
                    options.UseInMemoryDatabase("PixelDanceDb"));

                return services;
            }
#if MS_SERVER
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
#else
            services.AddDbContext<TContext>(options =>
                options.UseNpgsql(connectionString));
#endif

            return services;
        }

        public static IServiceCollection AddAggregateRepository<TContext, TEntity>(this IServiceCollection services)
            where TEntity : class, IAggregateRoot
            where TContext : DbContext
        {
            services.AddSingleton<IRepository<TEntity>, EfRepository<TContext, TEntity>>(
                sp => new EfRepository<TContext, TEntity>(sp.GetService<TContext>()));

            return services;
       }
    }
}