using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;

namespace ExampleApplication.SwaggerVersioning.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterHealthChecks(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://httpstat.us/200"), "HTTP 200");

            services.AddHealthChecks()
                .AddUrlGroup(opt =>
                {
                    opt.AddUri(new Uri("https://httpstat.us/404"));
                    opt.ExpectHttpCode(404);
                }, "HTTP 404 - Healthy");

            services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://httpstat.us/404"), "HTTP 404 - Degraded", HealthStatus.Degraded);

            services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://httpstat.us/404"), "HTTP 404 - Unhealthy", HealthStatus.Unhealthy);

            return services;
        }
    }
}
