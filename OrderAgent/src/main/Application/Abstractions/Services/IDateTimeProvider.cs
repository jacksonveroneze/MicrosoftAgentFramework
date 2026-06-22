namespace JacksonVeroneze.OrderAgent.Application.Abstractions.Services;

public interface IDateTimeProvider
{
    public DateTimeOffset UtcNow { get; }

    public DateTimeOffset Now { get; }

    public DateOnly DateNow { get; }

    public TimeOnly TimeNow { get; }
}
