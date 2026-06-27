using JacksonVeroneze.OrderAgent.Agent.Extensions;
using JacksonVeroneze.OrderAgent.Agent.Services;
using JacksonVeroneze.OrderAgent.Api.Middlewares;
using JacksonVeroneze.OrderAgent.Api.Services;
using JacksonVeroneze.OrderAgent.Infrastructure.Configurations;
using JacksonVeroneze.OrderAgent.Infrastructure.Extensions;
using OpenTelemetry.Logs;

namespace JacksonVeroneze.OrderAgent.Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder Configure()
        {
            builder.Services.AddAppConfigs(builder.Configuration);

            var appConfiguration = builder.Configuration
                .Get<AppConfiguration>()!;

            builder.ConfigureDefaultServices(appConfiguration, builder.Environment);

            // builder.AddLogger(appConfiguration);

            builder.Logging.ClearProviders()
                .AddOpenTelemetry(options =>
                {
                    options.IncludeFormattedMessage = true;
                    options.IncludeScopes = true;
                    options.ParseStateValues = true;
                    options.AddConsoleExporter();
                });

            return builder;
        }

        private WebApplicationBuilder ConfigureDefaultServices(
            AppConfiguration appConfiguration,
            IHostEnvironment hostEnvironment
        )
        {
            builder.Services
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUserContext, HttpCurrentUserContext>()
                .AddProblemDetails()
                .AddExceptionHandler<CustomExceptionHandler>()
                .AddAuthentication(appConfiguration)
                .AddAuthorization(appConfiguration)
                .AddJsonOptionsSerialize()
                .AddAppVersioning()
                .AddRouting()
                .AddCorrelation()
                .AddApplicationServices()
                .AddFluentValidation(AssemblyReference.Assembly)
                .AddFluentValidation(Application.AssemblyReference.Assembly)
                .AddMapper(AssemblyReference.Assembly)
                .AddCached(appConfiguration)
                .AddOpenTelemetry(appConfiguration)
                .AddDatabase(appConfiguration, builder.Environment)
                .AddOllamaChatClient(appConfiguration, hostEnvironment)
                .AddOrdersAgent()
                .AddHealthChecks();

            return builder;
        }
    }
}
