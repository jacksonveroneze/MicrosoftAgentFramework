using FluentValidation;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

public sealed class GetOrdersByAssetUseCase(
    IValidator<GetOrdersByAssetRequest> validator,
    IOrderRepository repository) : IGetOrdersByAssetUseCase
{
    public async Task<Result<CheckOrdersByAssetResponse>> ExecuteAsync(
        GetOrdersByAssetRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await validator.ValidateAndThrowAsync(
            request, cancellationToken);
        
        var total = await repository
            .CountByTickerAsync(
                request.AccountId,
                request.UserId,
                request.Ticker,
                cancellationToken);

        CheckOrdersByAssetResponse response = new(
            request.Ticker,
            total > 0,
            total);

        return Result<CheckOrdersByAssetResponse>
            .WithSuccess(response);
    }
}
