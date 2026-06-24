namespace JacksonVeroneze.OrderAgent.Application.Abstractions.Services;

public interface ICurrentUserContext
{
    Guid UserId { get; }

    Guid AccountId { get; }
}
