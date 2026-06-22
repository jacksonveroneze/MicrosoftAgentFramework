namespace JacksonVeroneze.OrderAgent.Api.Security;

public static class AuthorizationPolicies
{
    public const string JwtAccess = "JwtAccess";
    public const string ProfilesCreate = "ProfilesCreate";
    public const string ProfilesActivate = "ProfilesActivate";
    public const string ProfilesInactivate = "ProfilesInactivate";
    public const string ProfilesRead = "ProfilesRead";
    
    public const string McpAccess = "McpAccess";
    public const string McpProfilesCreate = "McpProfilesCreate";
    public const string McpProfilesActivate = "McpProfilesActivate";
    public const string McpProfilesInactivate = "McpProfilesInactivate";
    public const string McpProfilesRead = "McpProfilesRead";
}
