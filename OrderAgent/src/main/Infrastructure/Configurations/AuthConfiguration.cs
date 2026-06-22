using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AuthConfiguration
{
    [Required]
    public string? Authority { get; init; }
    
    [Required]
    public string? Audience { get; init; }

    [Required]
    public string? Issuer { get; set; }
}
