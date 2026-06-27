namespace JacksonVeroneze.OrderAgent.Agent.Sanitizers;

internal static class TickerSanitizer
{
    internal static string Normalize(string ticker)
    {
        return string.IsNullOrWhiteSpace(ticker)
            ? string.Empty
            : ticker.Trim().ToUpperInvariant();
    }
}
