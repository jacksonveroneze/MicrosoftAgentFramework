using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.OrderAgent.Domain.Errors;

public static class OrderErrors
{
    public static class OrderError
    {
        public static Error NotFound =>
            Error.Create("Order.NotFound",
                "The order with the specified identifier was not found.");
    }
}
