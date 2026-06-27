namespace JacksonVeroneze.OrderAgent.Agent.Models.Orders.Agent;

public sealed record OrdersAgentOutput(
    string Message,
    int MessageCount,
    IReadOnlyCollection<string> Messages,
    string? RawText);
