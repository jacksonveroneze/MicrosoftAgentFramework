using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;
using JacksonVeroneze.OrderAgent.Infrastructure.Contexts;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Repositories.Profile;

[ExcludeFromCodeCoverage]
public class OrderRepository(
    IEfCoreRepository<Domain.Entities.Order, DefaultDbContext> efRepository)
    : IOrderRepository
{
    #region read

    public async Task<Domain.Entities.Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await efRepository.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        return result;
    }

    #endregion
}
