using JacksonVeroneze.OrderAgent.Agent.Instructions;
using JacksonVeroneze.OrderAgent.Agent.Middleware;
using JacksonVeroneze.OrderAgent.Agent.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.OrderAgent.Agent.Agents;

internal sealed class OrdersAgentBuilder(
    IChatClient chatClient,
    CheckOrdersByAssetTool checkOrdersByAssetTool,
    IServiceProvider serviceProvider,
    ILoggerFactory loggerFactory)
{
    private const string AgentName = "orders-agent";
    private const string AgentDescription = "Agent de consulta de existência de ordens por ativo.";

    internal AIAgent Build()
    {
        var baseAgent = new ChatClientAgent(
            chatClient,
            new ChatClientAgentOptions
            {
                Name = AgentName,
                Description = AgentDescription,
                ChatOptions = new ChatOptions
                {
                    Instructions = OrdersAgentInstructions.SystemPrompt,
                    Tools =
                    [
                        BuildCheckOrdersByAssetFunction(),
                    ],
                    ToolMode = ChatToolMode.Auto,
                    AllowMultipleToolCalls = false,
                    Temperature = 0,
                },
            }, loggerFactory, serviceProvider);

        var agent = baseAgent
            .AsBuilder()
            .Use(runFunc: OrdersAgentGuardrails.ValidateAgentRunAsync, runStreamingFunc: null)
            .Use(OrdersAgentGuardrails.ValidateFunctionCallAsync)
            .Build();

        return agent;
    }

    private AIFunction BuildCheckOrdersByAssetFunction()
    {
        AIFunction checkOrdersByAssetFunction = AIFunctionFactory.Create(
            checkOrdersByAssetTool.CheckOrdersByAssetAsync,
            new AIFunctionFactoryOptions
            {
                Name = OrdersAgentToolConsts.CheckOrdersByAssetName,
                Description = OrdersAgentToolConsts.CheckOrdersByAssetDescription,
            });

        return checkOrdersByAssetFunction;
    }
}
