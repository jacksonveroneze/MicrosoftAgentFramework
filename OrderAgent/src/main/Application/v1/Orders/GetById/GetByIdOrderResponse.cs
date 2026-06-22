using JacksonVeroneze.OrderAgent.Application.Common.Models.Common.Response;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Models;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetById;

public sealed record GetByIdOrderResponse
    : DataResponse<OrderResponse>;
