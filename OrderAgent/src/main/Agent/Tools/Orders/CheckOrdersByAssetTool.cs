using System.ComponentModel;
using JacksonVeroneze.OrderAgent.Agent.Models.Orders;
using JacksonVeroneze.OrderAgent.Agent.Sanitizers;
using JacksonVeroneze.OrderAgent.Agent.Validators;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

namespace JacksonVeroneze.OrderAgent.Agent.Tools.Orders;

internal sealed class CheckOrdersByAssetTool(
    ICurrentUserContext currentUserContext,
    IGetOrdersByAssetUseCase useCase)
{
    private const string ParamTickerDescription =
        "Ticker do ativo informado pelo usuário. Exemplos: PETR4, VALE3, ITUB4.";

    private const string ErrorParamTickerInvalid =
        "Ticker inválido. Informe um ticker de ativo válido, como PETR4, VALE3 ou ITUB4.";

    private const string ErrorGeneric =
        "Não foi possível consultar as ordens neste momento.";

    private const string TemplateHasOrders = "O usuário possui {0} ordem(ns) para {1}.";
    private const string TemplateNoHasOrders = "O usuário não possui ordens para {0}.";

    [Description(OrdersAgentToolConsts.CheckOrdersByAssetDescription)]
    internal async Task<CheckOrdersByAssetToolResult> CheckOrdersByAssetAsync(
        [Description(ParamTickerDescription)] string assetTicker,
        CancellationToken cancellationToken)
    {
        var normalizedTicker = TickerSanitizer.Normalize(assetTicker);

        if (!TickerValidator.IsValid(normalizedTicker))
        {
            return new CheckOrdersByAssetToolResult(
                Success: false,
                AssetTicker: normalizedTicker,
                HasOrders: false,
                OrdersCount: 0,
                Message: ErrorParamTickerInvalid);
        }

        var request = new GetOrdersByAssetRequest(
            currentUserContext.AccountId,
            currentUserContext.UserId,
            normalizedTicker);

        var result = await useCase.ExecuteAsync(
            request, cancellationToken);

        if (!result.IsSuccess || result.Value is null)
        {
            return new CheckOrdersByAssetToolResult(
                Success: false,
                AssetTicker: normalizedTicker,
                HasOrders: false,
                OrdersCount: 0,
                Message: ErrorGeneric);
        }

        return new CheckOrdersByAssetToolResult(
            Success: true,
            AssetTicker: result.Value!.AssetTicker,
            HasOrders: result.Value.HasOrders,
            OrdersCount: result.Value.OrdersCount,
            Message: result.Value.HasOrders
                ? string.Format(TemplateHasOrders, result.Value.OrdersCount, result.Value.AssetTicker)
                : string.Format(TemplateNoHasOrders, result.Value.AssetTicker));
    }
}
