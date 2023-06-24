using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ExampleApplication.SwaggerVersioning.Utils
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseHealthChecks(this WebApplication app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.MapHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.MapHealthChecksUI(options =>
            {
                options.UIPath = "/healthchecks-ui";
            });

            return app;
        }
    }
}
