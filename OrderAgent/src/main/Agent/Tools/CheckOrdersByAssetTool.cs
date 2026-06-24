using System.ComponentModel;
using JacksonVeroneze.OrderAgent.Agent.Models;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Services;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

namespace JacksonVeroneze.OrderAgent.Agent.Tools;

public sealed class CheckOrdersByAssetTool(
    IGetOrdersByAssetUseCase useCase,
    ICurrentUserContext userContext)
{
    private const string ParamTickerDescription =
        "Ticker do ativo informado pelo usuário. Exemplos: PETR4, VALE3, ITUB4.";

    private const string ErrorParamTickerInvalid =
        "Ticker inválido. Informe um ticker de ativo válido, como PETR4, VALE3 ou ITUB4.";

    private const string TemplateHasOrdes = "O usuário possui {0} ordem(ns) para {1}.";
    private const string TemplateNoHasOrdes = "O usuário não possui ordens para {0}.";

    [Description(OrdersAgentToolConsts.CheckOrdersByAssetDescription)]
    public async Task<CheckOrdersByAssetToolResult> CheckOrdersByAssetAsync(
        [Description(ParamTickerDescription)] string assetTicker,
        CancellationToken cancellationToken = default)
    {
        var normalizedTicker = NormalizeTicker(assetTicker);

        if (!IsValidTicker(normalizedTicker))
        {
            return new CheckOrdersByAssetToolResult(
                Success: false,
                AssetTicker: normalizedTicker,
                HasOrders: false,
                OrdersCount: 0,
                Message: ErrorParamTickerInvalid);
        }

        GetOrdersByAssetRequest request = new(
            normalizedTicker,
            userContext.UserId);

        var result = await useCase.ExecuteAsync(
            request, cancellationToken).ConfigureAwait(false);

        return new CheckOrdersByAssetToolResult(
            Success: true,
            AssetTicker: result.Value!.AssetTicker,
            HasOrders: result.Value.HasOrders,
            OrdersCount: result.Value.OrdersCount,
            Message: result.Value.HasOrders
                ? string.Format(TemplateHasOrdes, result.Value.OrdersCount, result.Value.AssetTicker)
                : string.Format(TemplateNoHasOrdes, result.Value.AssetTicker));
    }

    private static string NormalizeTicker(string ticker) =>
        string.IsNullOrWhiteSpace(ticker)
            ? string.Empty
            : ticker.Trim().ToUpperInvariant();

    private static bool IsValidTicker(string ticker) =>
        ticker.Length is >= 4 and <= 10 &&
        ticker.All(char.IsLetterOrDigit);
}
