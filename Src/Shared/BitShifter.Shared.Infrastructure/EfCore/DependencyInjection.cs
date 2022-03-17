using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BitShifter.Shared.Infrastructure.EfCore.Seeder;

namespace BitShifter.Shared.Infrastructure.EfCore
{
    public static class DependencyInjection
    {
        internal static IServiceCollection AddEfCore(this IServiceCollection services)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }

        internal static IApplicationBuilder UseEfCore(this IApplicationBuilder app)
        {
            SqlSeeder.SeedSampleData(
                app.ApplicationServices, 
                AppDomain.CurrentDomain.GetAssemblies());

            return app;
        }
    }
}