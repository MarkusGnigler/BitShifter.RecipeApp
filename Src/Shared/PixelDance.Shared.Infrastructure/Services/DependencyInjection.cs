using Microsoft.Extensions.DependencyInjection;
using PixelDance.Shared.Abstractions.Interfaces;

namespace PixelDance.Shared.Infrastructure.Services
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddSingleton<IWebRootWatcher, WebRootWatcher>();
            services.AddSingleton<IFileService, FileService>();

            return services;
        }
    }
}
