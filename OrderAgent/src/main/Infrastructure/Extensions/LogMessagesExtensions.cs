using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Extensions;

public static partial class LogMessagesExtensions
{
    [LoggerMessage(
        EventId = 1041,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - IsFound: {isFound}")]
    public static partial void LogGetById(this ILogger logger,
        string contextName, string className,
        string methodName, Guid? identifier, bool isFound);

    [LoggerMessage(
        EventId = 1042,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - Exists: {exists}")]
    public static partial void LogExistsByValue(this ILogger logger,
        string contextName, string className,
        string methodName, string? identifier, bool exists);
}
