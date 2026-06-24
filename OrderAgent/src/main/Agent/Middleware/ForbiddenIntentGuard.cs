using JacksonVeroneze.OrderAgent.Agent.Models;
using JacksonVeroneze.OrderAgent.Agent.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace JacksonVeroneze.OrderAgent.Agent.Middleware;

internal static class OrdersAgentGuardrails
{
    private static readonly string[] ForbiddenPatterns =
    [
        "compre", "comprar", "compra", "venda", "vende", "vender",
        "enviar ordem", "envie ordem", "coloca ordem", "coloque ordem",
        "cancele", "cancelar", "cancelamento",
        "altere", "alterar", "modificar", "modifique",
        "recomend", "devo comprar", "devo vender", "vale a pena",
        "carteira", "posição", "patrimônio",
        "provento", "dividendo",
        "chamado", "suporte", "reclamação",
        "perfil de risco", "alteração cadastral", "dados cadastrais",
        "ignore", "esqueça", "desconsidere", "nova instrução",
        "você agora é", "finja que", "aja como", "pretend",
        "ignore previous", "forget previous", "disregard"
    ];

    internal static Task<AgentResponse> ValidateAgentRunAsync(
        IEnumerable<ChatMessage> messages,
        AgentSession? session,
        AgentRunOptions? options,
        AIAgent innerAgent,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(messages);
        ArgumentNullException.ThrowIfNull(innerAgent);

        var userMessage = messages
            .LastOrDefault(message => message.Role == ChatRole.User)
            ?.Text ?? string.Empty;

        if (!HasForbiddenIntent(userMessage))
        {
            return innerAgent.RunAsync(
                messages, session, options, cancellationToken);
        }

        AgentResponse response = new(
        [
            new ChatMessage(
                ChatRole.Assistant,
                OrdersAgentResponseMessages.SafeRefusalMessage),
        ]);

        return Task.FromResult(response);

    }

    internal static async ValueTask<object?> ValidateFunctionCallAsync(
        AIAgent agent,
        FunctionInvocationContext context,
        Func<FunctionInvocationContext, CancellationToken, ValueTask<object?>> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(agent);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        if (!string.Equals(
                context.Function.Name,
                OrdersAgentToolConsts.CheckOrdersByAssetName,
                StringComparison.Ordinal))
        {
            return new CheckOrdersByAssetToolResult(
                Success: false,
                AssetTicker: string.Empty,
                HasOrders: false,
                OrdersCount: 0,
                Message: OrdersAgentResponseMessages.SafeRefusalMessage);
        }

        return await next(context, cancellationToken).ConfigureAwait(false);
    }

    private static bool HasForbiddenIntent(string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage))
        {
            return false;
        }

        return ForbiddenPatterns.Any(pattern =>
            userMessage.Contains(pattern, StringComparison.OrdinalIgnoreCase));
    }
}
