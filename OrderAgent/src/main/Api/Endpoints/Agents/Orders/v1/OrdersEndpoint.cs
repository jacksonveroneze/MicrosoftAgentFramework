using Asp.Versioning;
using FluentValidation;
using JacksonVeroneze.OrderAgent.Agent.Agents.Orders;
using JacksonVeroneze.OrderAgent.Agent.Models.Orders.Agent;
using JacksonVeroneze.OrderAgent.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using OrdersAgentRequest = JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Models.OrdersAgentRequest;
using OrdersAgentResponse = JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Models.OrdersAgentResponse;

namespace JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1;

internal static class OrdersEndpoint
{
    private const string Resource = "orders/messages";
    private const int Version = 1;

    public static WebApplication AddOrdersEndpoints(
        this WebApplication app)
    {
        var apiVersion = app.NewApiVersionSet()
            .ReportApiVersions()
            .HasApiVersion(
                new ApiVersion(Version))
            .Build();

        RouteGroupBuilder builder =
            app.MapGroup("agents/v{version:apiVersion}/" + Resource)
                .WithTags(Resource)
                .WithApiVersionSet(apiVersion)
                .MapToApiVersion(Version);

        builder.AddOrderChatAgentEndpoint();

        return app;
    }

    private static RouteGroupBuilder AddOrderChatAgentEndpoint(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] IOrdersAgent agent,
                [FromServices] IValidator<OrdersAgentRequest> validator,
                [FromServices] IHostEnvironment hostEnvironment,
                [FromBody] OrdersAgentRequest input,
                CancellationToken cancellationToken) =>
            {
                var validationResult = await validator
                    .ValidateAsync(input, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return validationResult
                        .ToValidationProblem();
                }

                var agentRequest = new OrdersAgentInput(input.Prompt);

                var agentResponse = await agent.RunAsync(
                    agentRequest, cancellationToken);

                var output = agentResponse
                    .ToHttpResponse(hostEnvironment);

                return Results.Ok(output);
            })
            .Produces<OrdersAgentResponse>(
                statusCode: StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
