using JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetById;

public sealed record GetByIdOrderRequest(Guid Id)
    : IBaseRequest;
