namespace JacksonVeroneze.OrderAgent.Agent.Validators;

internal static class TickerValidator
{
    internal static bool IsValid(string ticker)
    {
        return !string.IsNullOrEmpty(ticker)
               && ticker.Length is >= 4 and <= 10
               && ticker.All(char.IsLetterOrDigit);
    }
}
