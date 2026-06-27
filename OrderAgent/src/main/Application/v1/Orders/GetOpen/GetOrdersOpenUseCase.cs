using FluentValidation;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Filters;
using JacksonVeroneze.OrderAgent.Domain.Enums;
using MapsterMapper;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetOpen;

public sealed class GetOrdersOpenUseCase(
    IMapper mapper,
    IValidator<GetOrdersOpenRequest> validator,
    IOrderRepository repository) : IGetOrdersOpenUseCase
{
    public async Task<Result<GetOrdersOpenResponse>> ExecuteAsync(
        GetOrdersOpenRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        await validator.ValidateAndThrowAsync(
            request, cancellationToken);

        var filter = new OrderFilter
        {
            AccountId = request.AccountId,
            UserId = request.UserId,
            Status = OrderStatus.Open,
        };

        var orders = await repository
            .GetByFilterAsync(filter, cancellationToken);

        var response = mapper.Map<GetOrdersOpenResponse>(orders);

        return Result<GetOrdersOpenResponse>
            .WithSuccess(response);
    }
}
