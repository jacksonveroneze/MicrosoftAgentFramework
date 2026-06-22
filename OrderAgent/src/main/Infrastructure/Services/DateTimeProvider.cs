using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Services;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Services;

[ExcludeFromCodeCoverage]
internal sealed class DateTimeProvider : IDateTimeProvider
{
    private const string WindowsBrTimeZone
        = "E. South America Standard Time";

    private const string LinuxBrTimeZone
        = "America/Sao_Paulo";

    private static readonly TimeZoneInfo TzInfo
        = GetTimeZoneInfo();

    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    public DateTimeOffset Now => TimeZoneInfo
        .ConvertTime(UtcNow, TzInfo);

    public DateOnly DateNow =>
        DateOnly.FromDateTime(Now.LocalDateTime);

    public TimeOnly TimeNow =>
        TimeOnly.FromDateTime(Now.LocalDateTime);

    private static TimeZoneInfo GetTimeZoneInfo()
    {
        return TimeZoneInfo
            .FindSystemTimeZoneById(GetTimeZoneId());
    }

    private static string GetTimeZoneId()
    {
        if (OperatingSystem.IsWindows())
        {
            return WindowsBrTimeZone;
        }

        return OperatingSystem.IsLinux()
               || OperatingSystem.IsMacOS()
            ? LinuxBrTimeZone
            : throw new InvalidOperationException("Invalid plataform");
    }
}
