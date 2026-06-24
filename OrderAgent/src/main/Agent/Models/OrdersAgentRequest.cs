using Microsoft.Agents.AI;

namespace JacksonVeroneze.OrderAgent.Agent.Models;

public sealed record OrdersAgentRequest(
    string UserMessage,
    AgentSession? Session = null);
