using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Shared.Infrastructure.MediatR.Behaviours;

namespace PixelDance.Shared.Infrastructure.MediatR
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}
