using JacksonVeroneze.NET.Result;
using JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetById;

public interface IGetByIdOrderUseCase :
    IUseCase<GetByIdOrderRequest, Result<GetByIdOrderResponse>>;
