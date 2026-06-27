using System.Security.Claims;
using JacksonVeroneze.OrderAgent.Agent;

namespace JacksonVeroneze.OrderAgent.Api.Services;

internal sealed class HttpCurrentUserContext(
    IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    private const string ClaimUserId = "user_id";
    private const string ClaimAccountId = "account_id";
    private const string HeaderUserId = "X-User-Id";
    private const string HeaderAccountId = "X-Account-Id";

    public Guid UserId => GetGuidValue(
        ClaimUserId,
        ClaimTypes.NameIdentifier,
        "sub",
        HeaderUserId);

    public Guid AccountId => GetGuidValue(
        ClaimAccountId,
        HeaderAccountId);

    private Guid GetGuidValue(params string[] claimTypesOrHeaderNames)
    {
        HttpContext httpContext =
            httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HTTP context is not available.");

        foreach (var claimTypeOrHeaderName in claimTypesOrHeaderNames)
        {
            var claimValue = httpContext.User
                .FindFirstValue(claimTypeOrHeaderName);

            if (Guid.TryParse(claimValue, out var claimGuid))
            {
                return claimGuid;
            }

            if (httpContext.Request.Headers.TryGetValue(
                    claimTypeOrHeaderName, out var headerValue)
                && Guid.TryParse(headerValue.ToString(), out var headerGuid))
            {
                return headerGuid;
            }
        }

        throw new InvalidOperationException(
            $"Could not resolve a valid Guid from claims/headers: " +
            $"{string.Join(", ", claimTypesOrHeaderNames)}.");
    }
}
