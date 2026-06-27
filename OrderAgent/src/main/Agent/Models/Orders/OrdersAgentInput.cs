using Microsoft.Agents.AI;

namespace JacksonVeroneze.OrderAgent.Agent.Models.Orders;

public sealed record OrdersAgentInput(
    string Prompt,
    AgentSession? Session = null);
