using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Filters;
using JacksonVeroneze.OrderAgent.Infrastructure.Builders.Filters;
using JacksonVeroneze.OrderAgent.Infrastructure.Contexts;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Repositories.Order;

[ExcludeFromCodeCoverage]
public class OrderRepository(
    IEfCoreRepository<Domain.Entities.Order, DefaultDbContext> efRepository)
    : IOrderRepository
{
    public async Task<Domain.Entities.Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await efRepository.GetByIdAsync(
            order => order.Id == id,
            cancellationToken);

        return result;
    }

    public async Task<int> CountByTickerAsync(
        Guid accountId,
        Guid userId,
        string ticker,
        CancellationToken cancellationToken)
    {
        var result = await efRepository.CountAsync(
            order => order.AccountId == accountId
                     && order.UserId == userId
                     && order.Ticker == ticker,
            cancellationToken);

        return (int)result;
    }

    public Task<ICollection<Domain.Entities.Order>> GetByFilterAsync(
        OrderFilter filter, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var expression = OrderFilterBuilder
            .Create(filter).Build();

        var result = efRepository.GetAllAsync(
            expression,
            order => order.Id,
            100,
            cancellationToken: cancellationToken);

        return result;
    }
}
