using JacksonVeroneze.OrderAgent.Api.Middlewares;
using JacksonVeroneze.OrderAgent.Infrastructure.Configurations;
using JacksonVeroneze.OrderAgent.Infrastructure.Extensions;

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

            builder.ConfigureDefaultServices(appConfiguration);

            builder.AddLogger(appConfiguration);

            return builder;
        }

        private WebApplicationBuilder ConfigureDefaultServices(
            AppConfiguration appConfiguration)
        {
            builder.Services.AddMcp(appConfiguration);

            builder.Services
                .AddProblemDetails()
                .AddExceptionHandler<CustomExceptionHandler>()
                .AddAuthentication(builder.Configuration, appConfiguration)
                .AddAuthorization(appConfiguration)
                .AddRouting()
                .AddCorrelation()
                .AddApplicationServices()
                .AddFluentValidation(AssemblyReference.Assembly)
                .AddMapper(AssemblyReference.Assembly)
                .AddCached(appConfiguration)
                .AddOpenTelemetry(appConfiguration)
                .AddDatabase(appConfiguration, builder.Environment)
                .AddHealthChecks();

            return builder;
        }
    }
}
