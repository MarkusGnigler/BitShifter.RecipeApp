using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BitShifter.Shared.Abstractions.EfCore.Seeder;
using BitShifter.Modules.Identity.Core.Persistence.Seeding;

namespace BitShifter.Modules.Identity.Core.Persistence
{
    internal class ContextSeeder : ISeeder
    {
        public async Task SeedSampleData(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetService<IdentityDbContext>();

            if (context is null) return;

            await context.Database.EnsureCreatedAsync();

            await SeedSampleDataAsync(context, serviceProvider);
        }

        private static async Task SeedSampleDataAsync(IdentityDbContext context, IServiceProvider serviceProvider)
        {
            await UserSeed.SeedUserRoles(context, serviceProvider);
            await UserSeed.SeedAdminUser(context, serviceProvider);
        }
    }
}
