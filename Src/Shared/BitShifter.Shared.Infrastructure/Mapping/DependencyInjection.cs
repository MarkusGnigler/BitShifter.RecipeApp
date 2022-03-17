using Microsoft.Extensions.DependencyInjection;

namespace BitShifter.Shared.Infrastructure.Mapping
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}
