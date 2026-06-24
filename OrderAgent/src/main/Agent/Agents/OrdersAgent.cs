using JacksonVeroneze.OrderAgent.Agent.Models;
using Microsoft.Agents.AI;

namespace JacksonVeroneze.OrderAgent.Agent.Agents;

public sealed class OrdersAgent(AIAgent agent) : IOrdersAgent
{
    public async Task<OrdersAgentResponse> RunAsync(
        OrdersAgentRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var response = await agent
            .RunAsync(request.UserMessage,
                session: request.Session,
                options: null,
                cancellationToken: cancellationToken);

        var result = string.IsNullOrWhiteSpace(response.Text)
            ? OrdersAgentResponseMessages.SafeRefusalMessage
            : response.Text;

        return new OrdersAgentResponse(result, response.Messages.Count);
    }
}
