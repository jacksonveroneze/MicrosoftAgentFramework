using JacksonVeroneze.OrderAgent.Agent.Models.Orders.Agent;

namespace JacksonVeroneze.OrderAgent.Agent.Agents.Orders;

public interface IOrdersAgent
{
    Task<OrdersAgentOutput> RunAsync(
        OrdersAgentInput input,
        CancellationToken cancellationToken);
    
    IAsyncEnumerable<OrdersAgentStreamEvent> RunStreamAsync(
        SendOrdersAgentMessageRequest request,
        CancellationToken cancellationToken);
}

public sealed record SendOrdersAgentMessageRequest(
    string Message);

public sealed record OrdersAgentStreamEvent(
    string Type,
    string Text);
