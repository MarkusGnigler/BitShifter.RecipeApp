using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using BitShifter.Modules.Recipes.Api;
using BitShifter.Modules.Identity.Api;
using BitShifter.Shared.Abstractions;
using BitShifter.Shared.Infrastructure;
using BitShifter.Shared.Infrastructure.Bootstrapper;
using BitShifter.Shared.Infrastructure.Bootstrapper.Filters;
using BitShifter.Modules.Identity.Api.Extensions;

namespace BitShifter.Bootstrapper
{
    public class Startup
    {
        private IServiceCollection? _services;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddControllers(options =>
            //    options
            //        .AddIdentityFilters()
            //        .AddCoreFilters()
            //        );

            services.AddBsCors(_configuration);

            services
                .AddRecipesModule(_configuration)
                .AddIdentityModule(_configuration)

                .AddAbstractions()
                .AddInfrastructure();

            _services = services;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                RegisteredServicesPage(app);
            }

            app
                .UseAbstractions()
                .UseInfrastructure()

                .UseRouting()
                .UsePxdCors()

                .UseRecipesModule()
                .UseIdentityModule();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("PixelDance API"));
            });
        }

        private void RegisteredServicesPage(IApplicationBuilder app)
        {
            if (_services is null) return;

            app.Map("/services", builder => builder.Run(async context =>
            {
                var sb = new System.Text.StringBuilder();

                sb.Append("<h1>Registered Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");

                await context.Response.WriteAsync(sb.ToString());
            }));

        }
    }
}
