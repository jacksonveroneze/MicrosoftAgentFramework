namespace JacksonVeroneze.OrderAgent.Domain.Enums;

public enum OrderStatus
{
    None = 0,
    Pending = 1,
    Open = 2,
    PartiallyFilled = 3,
    Filled = 4,
    Cancelled = 5,
    Rejected = 6,
}
