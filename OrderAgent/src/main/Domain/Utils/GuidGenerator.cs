namespace JacksonVeroneze.OrderAgent.Domain.Utils;

public static class GuidGenerator
{
    public static Guid Generate()
    {
        return Guid.CreateVersion7();
    }
}
