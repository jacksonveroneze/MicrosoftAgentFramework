using JacksonVeroneze.NET.Result;
using JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

public interface IGetOrdersByAssetUseCase :
    IUseCase<GetOrdersByAssetRequest, Result<CheckOrdersByAssetResponse>>;
