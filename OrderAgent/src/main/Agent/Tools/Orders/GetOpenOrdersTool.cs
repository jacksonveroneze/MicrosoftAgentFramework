using System.ComponentModel;
using JacksonVeroneze.OrderAgent.Agent.Models.Orders.GetOpenOrders;
using JacksonVeroneze.OrderAgent.Agent.Services;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.GetOpen;

namespace JacksonVeroneze.OrderAgent.Agent.Tools.Orders;

internal sealed class GetOpenOrdersTool(
    ICurrentUserContext currentUserContext,
    IGetOrdersOpenUseCase useCase)
{
    internal const string ToolName = "get_open_orders";
    internal const string ToolDescription = 
        "Lista as orders da conta e usuário que estão no status aberto.";

    private const string ErrorGeneric = "Não foi possível consultar as ordens neste momento.";

    private const string TemplateHasOrders = "O usuário possui {0} ordem(ns) aberta(s).";
    private const string TemplateNoHasOrdersOpen = "O usuário não possui ordens abertas.";

    [Description(ToolDescription)]
    internal async Task<GetOpenOrdersToolResult> GetOpenOrdersAsync(
        CancellationToken cancellationToken)
    {
        var request = new GetOrdersOpenRequest(
            currentUserContext.AccountId,
            currentUserContext.UserId);

        var result = await useCase.ExecuteAsync(
            request, cancellationToken);

        if (!result.IsSuccess || result.Value is null)
        {
            return new GetOpenOrdersToolResult(
                Success: false,
                OrdersCount: 0,
                Orders: [],
                Message: ErrorGeneric);
        }

        var totalOrders = result.Value.Orders.Length;

        return new GetOpenOrdersToolResult(
            Success: true,
            OrdersCount: totalOrders,
            Orders: result.Value.Orders,
            Message: totalOrders > 0
                ? string.Format(TemplateHasOrders, totalOrders)
                : TemplateNoHasOrdersOpen);
    }
}
