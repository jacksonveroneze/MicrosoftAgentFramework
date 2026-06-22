using JacksonVeroneze.NET.DomainObjects.Domain;

namespace JacksonVeroneze.OrderAgent.Domain.Entities;

public sealed class Order : Entity
{
    public Guid Id { get; }

    #region Ctor

    public Order()
    {
    }

    #endregion
}
