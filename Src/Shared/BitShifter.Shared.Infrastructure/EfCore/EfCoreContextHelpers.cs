//#define MS_SERVER
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BitShifter.Shared.Kernel.Interfaces;
using BitShifter.Shared.Infrastructure.Utilities;
using BitShifter.Shared.Infrastructure.EfCore.Repository;
using BitShifter.Shared.Abstractions.EfCore.Repository;

namespace BitShifter.Shared.Infrastructure.EfCore
{
    public static class EfCoreContextHelpers
    {
        public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            var options = services.GetOptions<EfCoreOptions>("EfCore");
            var connectionString = options.ConnectionString;

            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
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