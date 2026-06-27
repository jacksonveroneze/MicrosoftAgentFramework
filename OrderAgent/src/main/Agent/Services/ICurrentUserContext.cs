namespace JacksonVeroneze.OrderAgent.Agent.Services;

public interface ICurrentUserContext
{
    Guid UserId { get; }

    Guid AccountId { get; }
}
