using JacksonVeroneze.OrderAgent.Api.Extensions;
using Serilog;

try
{
    Log.Information("Starting application");

    var builder =
        WebApplication.CreateBuilder(args);

    builder.Configure();

    var app = builder.Build();

    app.Configure();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");

    throw;
}
finally
{
    Log.Information("Server Shutting down");
    await Log.CloseAndFlushAsync();
}
