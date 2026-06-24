using JacksonVeroneze.NET.Result;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

public sealed class GetOrdersByAssetUseCase(
    IOrderRepository repository) : IGetOrdersByAssetUseCase
{
    public async Task<Result<CheckOrdersByAssetResponse>> ExecuteAsync(
        GetOrdersByAssetRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var total = await repository
            .CountByTickerAsync(
                request.AssetTicker,
                request.UserId,
                cancellationToken);

        CheckOrdersByAssetResponse response = new(
            request.AssetTicker,
            total > 0,
            total);

        return Result<CheckOrdersByAssetResponse>
            .WithSuccess(response);
    }
}
