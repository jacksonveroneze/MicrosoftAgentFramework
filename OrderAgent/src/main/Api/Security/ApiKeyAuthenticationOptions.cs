using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace JacksonVeroneze.OrderAgent.Api.Security;

public sealed class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public string HeaderName { get; set; } = ApiKeyAuthenticationDefaults.HeaderName;

    public List<ApiKeyCredentialOptions> ApiKeys { get; set; } = [];
}

public sealed class ApiKeyCredentialOptions
{
    public string ClientId { get; set; } = string.Empty;

    public string? ClientName { get; set; }

    public string KeyHash { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset? ExpiresAtUtc { get; set; }

    public List<string> Scopes { get; set; } = [];
}

public sealed class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headerName = string.IsNullOrWhiteSpace(Options.HeaderName)
            ? ApiKeyAuthenticationDefaults.HeaderName
            : Options.HeaderName;

        if (!Request.Headers.TryGetValue(headerName, out var headerValues))
        {
            return Task.FromResult(AuthenticateResult.Fail("API key was not provided."));
        }

        if (headerValues.Count != 1 || string.IsNullOrWhiteSpace(headerValues[0]))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key header."));
        }

        ApiKeyCredentialOptions? credential = FindAndVerifyKey(headerValues[0]!);

        if (credential is null)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));
        }

        var claims = CreateClaims(credential);
        var identity = new ClaimsIdentity(
            claims,
            Scheme.Name,
            ClaimTypes.Name,
            ClaimTypes.Role);

        var ticket = new AuthenticationTicket(
            new ClaimsPrincipal(identity),
            Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private ApiKeyCredentialOptions? FindAndVerifyKey(string apiKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        if (Options.ApiKeys.Count == 0)
        {
            return null;
        }

        var actualHash = SHA256.HashData(Encoding.UTF8.GetBytes(apiKey));
        DateTimeOffset now = TimeProvider.System.GetUtcNow();

        foreach (ApiKeyCredentialOptions credential in Options.ApiKeys)
        {
            if (!IsCredentialUsable(credential, now))
            {
                continue;
            }

            var expectedHash = TryParseSha256Hash(credential.KeyHash);

            if (expectedHash is null || expectedHash.Length != actualHash.Length)
            {
                continue;
            }

            if (CryptographicOperations.FixedTimeEquals(actualHash, expectedHash))
            {
                return credential;
            }
        }

        return null;
    }

    private static bool IsCredentialUsable(
        ApiKeyCredentialOptions credential,
        DateTimeOffset now)
    {
        return credential.IsEnabled
               && !string.IsNullOrWhiteSpace(credential.ClientId)
               && !string.IsNullOrWhiteSpace(credential.KeyHash)
               && (credential.ExpiresAtUtc is null || credential.ExpiresAtUtc > now);
    }

    private static byte[]? TryParseSha256Hash(string keyHash)
    {
        var normalizedHash = keyHash
            .Replace(":", string.Empty, StringComparison.Ordinal)
            .Replace("-", string.Empty, StringComparison.Ordinal)
            .Trim();

        if (normalizedHash.Length != 64)
        {
            return null;
        }

        try
        {
            return Convert.FromHexString(normalizedHash);
        }
        catch (FormatException)
        {
            return null;
        }
    }

    private static Claim[] CreateClaims(ApiKeyCredentialOptions credential)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, credential.ClientId),
            new(ClaimTypes.Name, credential.ClientName ?? credential.ClientId),
            new(ApiKeyAuthenticationClaimTypes.ClientId, credential.ClientId),
            new(ApiKeyAuthenticationClaimTypes.AuthenticationType, "api_key"),
        };

        claims.AddRange(credential.Scopes.Where(scope => !string.IsNullOrWhiteSpace(scope))
            .Distinct(StringComparer.Ordinal)
            .Select(scope => new Claim(ApiKeyAuthenticationClaimTypes.Scope, scope)));

        return [.. claims];
    }
}

public static class ApiKeyAuthenticationDefaults
{
    public const string AuthenticationScheme = "ApiKey";
    public const string HeaderName = "x-api-key";
}

public static class ApiKeyAuthenticationClaimTypes
{
    public const string AuthenticationType = "auth_type";
    public const string ClientId = "client_id";
    public const string Scope = "mcp_scope";
}

public static class ApiKeyAuthenticationExtensions
{
    public static AuthenticationBuilder AddApiKeyAuthenticationScheme(
        this AuthenticationBuilder authentication,
        IConfiguration configuration)
    {
        authentication.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
            ApiKeyAuthenticationDefaults.AuthenticationScheme,
            options => configuration
                .GetSection("Mcp:ApiKeyAuthentication")
                .Bind(options));

        return authentication;
    }
}
