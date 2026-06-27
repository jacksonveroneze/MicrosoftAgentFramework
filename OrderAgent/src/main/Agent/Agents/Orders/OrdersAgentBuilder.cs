using JacksonVeroneze.OrderAgent.Agent.Instructions.Orders;
using JacksonVeroneze.OrderAgent.Agent.Middleware;
using JacksonVeroneze.OrderAgent.Agent.Middleware.Orders;
using JacksonVeroneze.OrderAgent.Agent.Tools.Orders;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.OrderAgent.Agent.Agents.Orders;

internal sealed class OrdersAgentBuilder(
    IChatClient chatClient,
    CheckOrdersByTickerTool checkOrdersByTickerTool,
    GetOpenOrdersTool getOpenOrdersTool,
    AllowedToolsMiddleware allowedToolsMiddleware,
    IServiceProvider serviceProvider,
    ILoggerFactory loggerFactory,
    IHostEnvironment hostEnvironment)
{
    private const string AgentName = "orders-agent";
    private const string AgentDescription = "Agent de consulta de existência de ordens por ativo.";

    internal AIAgent Build()
    {
        var chatClientAgent = new ChatClientAgent(
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
                        BuildCheckOrdersByTickerFunction(),
                        BuildGetOpenOrdersFunction(),
                    ],
                    ToolMode = ChatToolMode.Auto,
                    AllowMultipleToolCalls = false,
                    Temperature = 0,
                },
            }, loggerFactory, serviceProvider);

        var agent = chatClientAgent
            .AsBuilder()
            .UseOpenTelemetry(
                sourceName: AgentName, 
                configure: cfg =>
                {
                    cfg.EnableSensitiveData = hostEnvironment.IsDevelopment();
                })
            .Use(runFunc: OrdersAgentGuardrails.ValidateAgentRunAsync, runStreamingFunc: null)
            .Use(allowedToolsMiddleware.ValidateFunctionCallAsync)
            .Build();

        return agent;
    }

    private AIFunction BuildCheckOrdersByTickerFunction()
    {
        AIFunction function = AIFunctionFactory.Create(
            checkOrdersByTickerTool.CheckOrdersByTickerAsync,
            new AIFunctionFactoryOptions
            {
                Name = CheckOrdersByTickerTool.ToolName,
                Description = CheckOrdersByTickerTool.ToolDescription,
            });

        return function;
    }
    
    private AIFunction BuildGetOpenOrdersFunction()
    {
        AIFunction function = AIFunctionFactory.Create(
            getOpenOrdersTool.GetOpenOrdersAsync,
            new AIFunctionFactoryOptions
            {
                Name = GetOpenOrdersTool.ToolName,
                Description = GetOpenOrdersTool.ToolDescription,
            });

        return function;
    }
}
