using System;
using System.Linq;
using System.Reflection;

using BitShifter.Shared.Infrastructure.Utilities;
using BitShifter.Shared.Abstractions.EfCore.Seeder;

namespace BitShifter.Shared.Infrastructure.EfCore.Seeder
{
    internal static class SqlSeeder
    {
        public static void SeedSampleData(IServiceProvider serviceProvider, params Assembly[] assemblies)
        {
            string companyName = typeof(SqlSeeder).FullName
                .Split('.')
                .First();

            var companyAssemblies = assemblies
                .Where(t => t.FullName.StartsWith(companyName))
                .ToArray();

            AssemblyScanner.Scan<ISeeder>(
                SeedSampleData(serviceProvider),
                companyAssemblies);
        }

        private static Action<ISeeder> SeedSampleData(IServiceProvider serviceProvider)
            => seeder =>
            {
                try
                {
                    seeder.SeedSampleData(serviceProvider);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during migration! ${ex.Message}");
                }
            };
    }
}
