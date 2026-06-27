namespace JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Models;

public sealed record AgentDebugResponse(
    int MessageCount,
    IReadOnlyCollection<string> Messages,
    string? RawText);
