using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitShifter.Shared.Infrastructure.Utilities
{
    public static class ConfigurationExtensions
    {
        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            IConfigurationSection section = configuration.GetSection(sectionName);

            var options = new T();

            section.Bind(options);

            return options;
        }

        public static string GetConfigurationValue(this IServiceCollection services, string sectionName)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            return configuration[sectionName];
        }
    }
}
