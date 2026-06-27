using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Filters;
using JacksonVeroneze.OrderAgent.Domain.Entities;

namespace JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<int> CountByTickerAsync(
        Guid accountId,
        Guid userId,
        string ticker,
        CancellationToken cancellationToken);
    
    Task<ICollection<Order>> GetByFilterAsync(
        OrderFilter filter,
        CancellationToken cancellationToken);
}
