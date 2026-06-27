using JacksonVeroneze.OrderAgent.Agent.Models;
using JacksonVeroneze.OrderAgent.Agent.Models.Orders.Agent;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace JacksonVeroneze.OrderAgent.Agent.Middleware;

internal sealed class AllowedToolsMiddleware(
    IReadOnlySet<string> allowedFunctions)
{
    private const string ToolNotAllowedCode = "tool_not_allowed";

    internal ValueTask<object?> ValidateFunctionCallAsync(
        AIAgent agent,
        FunctionInvocationContext context,
        Func<FunctionInvocationContext, CancellationToken, ValueTask<object?>> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(agent);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        if (allowedFunctions.Contains(context.Function.Name))
        {
            return next(context, cancellationToken);
        }

        var result = new ToolBlockedResult(
            Success: false,
            Code: ToolNotAllowedCode,
            Message: OrdersAgentOutputMessages.SafeRefusalMessage);

        return ValueTask.FromResult<object?>(result);
    }
}
