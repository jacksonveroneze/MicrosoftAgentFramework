namespace JacksonVeroneze.OrderAgent.Agent;

public interface ICurrentUserContext
{
    Guid UserId { get; }

    Guid AccountId { get; }
}
