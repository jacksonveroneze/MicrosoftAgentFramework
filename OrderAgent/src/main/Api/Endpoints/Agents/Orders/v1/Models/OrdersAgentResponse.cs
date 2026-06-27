namespace JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Models;

public sealed record OrdersAgentResponse(
    string Message,
    AgentDebugResponse? Debug = null);
