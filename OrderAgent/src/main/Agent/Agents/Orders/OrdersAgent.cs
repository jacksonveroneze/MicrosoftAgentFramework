using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using JacksonVeroneze.OrderAgent.Agent.Models.Orders.Agent;
using Microsoft.Agents.AI;

namespace JacksonVeroneze.OrderAgent.Agent.Agents.Orders;

internal sealed class OrdersAgent(AIAgent agent) : IOrdersAgent
{
    public async Task<OrdersAgentOutput> RunAsync(
        OrdersAgentInput input,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        AgentResponse response = await agent
            .RunAsync(input.Prompt,
                session: input.Session,
                cancellationToken: cancellationToken);

        var result = string.IsNullOrWhiteSpace(response.Text)
            ? OrdersAgentOutputMessages.SafeRefusalMessage
            : response.Text;

        var messages = response.Messages
            .SelectMany(item => item.Contents
                .Select(content => content.ToString()))
            .ToImmutableArray();

        return new OrdersAgentOutput(
            Message: result,
            MessageCount: response.Messages.Count,
            Messages: messages,
            RawText: response.Text);
    }

    public async IAsyncEnumerable<OrdersAgentStreamEvent> RunStreamAsync(
        SendOrdersAgentMessageRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var stream = agent.RunStreamingAsync(
            request.Message, cancellationToken: cancellationToken);

        await foreach (var update in stream.WithCancellation(cancellationToken))
        {
            if (!string.IsNullOrWhiteSpace(update.Text))
            {
                yield return new OrdersAgentStreamEvent(
                    Type: "message", Text: update.Text);
            }
        }

        yield return new OrdersAgentStreamEvent(
            Type: "completed",
            Text: string.Empty);
    }
}
