using JacksonVeroneze.NET.Result;
using JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetOpen;

public interface IGetOrdersOpenUseCase :
    IUseCase<GetOrdersOpenRequest, Result<GetOrdersOpenResponse>>;
