using CorrelationId;
using JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1;

namespace JacksonVeroneze.OrderAgent.Api.Extensions;

internal static class WebApplicationExtensions
{
    private const string PathHealth = "/health";
    private const string PathMetrics = "metrics";
    
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseCorrelationId();
        
        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseRouting();

        app.UseHealthChecks(PathHealth);
        app.UseOpenTelemetryPrometheusScrapingEndpoint(PathMetrics);

        //app.UseAuthentication();
        //app.UseAuthorization();
        
        app.AddOrdersEndpoints();

        return app;
    }
}
