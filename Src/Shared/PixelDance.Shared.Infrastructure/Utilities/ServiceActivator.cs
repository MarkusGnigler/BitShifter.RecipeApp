using System;
using Microsoft.Extensions.DependencyInjection;

namespace PixelDance.Shared.Infrastructure.Utilities
{
    //private static void GetServiceProvider()
    //{
    //    using (var scope = ServiceActivator.GetScope())
    //    {
    //        //IServiceProvider sp = ServiceActivator.GetServiceProvider();
    //        var serviceProvider = scope.ServiceProvider;
    //    }
    //}

    internal static class ServiceActivator
    {
        internal static IServiceProvider _serviceProvider;

        /// <summary>
        /// Configure ServiceActivator with full serviceProvider
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Create a scoped ServiceProvider
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(IServiceProvider serviceProvider = null)
        {
            using var serviceScope = ServiceActivator.GetScope(serviceProvider);

            return serviceScope.ServiceProvider;
        }

        /// <summary>
        /// Create a scope where use this ServiceActivator
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
        {
            var provider = serviceProvider ?? _serviceProvider;

            return provider?
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
        }

    }
}
