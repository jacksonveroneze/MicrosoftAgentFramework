using JacksonVeroneze.OrderAgent.Agent.Models.Orders.Agent;
using JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Models;
using OrdersAgentResponse = JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Models.OrdersAgentResponse;

namespace JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1;

internal static class ResponseMapper
{
    internal static OrdersAgentResponse ToHttpResponse(
        this OrdersAgentOutput agentOutput,
        IHostEnvironment hostEnvironment)
    {
        AgentDebugResponse? debugResponse = hostEnvironment.IsDevelopment()
            ? new AgentDebugResponse(
                MessageCount: agentOutput.MessageCount,
                Messages: agentOutput.Messages,
                RawText: agentOutput.RawText)
            : null;

        return new OrdersAgentResponse(
            Message: agentOutput.Message,
            Debug: debugResponse);
    }
}
