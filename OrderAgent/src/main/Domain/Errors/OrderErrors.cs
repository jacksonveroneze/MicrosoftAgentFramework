using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.OrderAgent.Domain.Errors;

public static class OrderErrors
{
    public static class OrderError
    {
        public static Error NotFound =>
            Error.Create("Order.NotFound",
                "The order with the specified identifier was not found.");

        public static Error AssetNotFound =>
            Error.Create("Order.AssetNotFound",
                "No orders were found for the specified asset ticker.");
    }
}
