using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.OrderAgent.Domain.Errors;

public static class DomainErrors
{
    public static class ProfileError
    {
        public static Error NotFound =>
            Error.Create("Profile.NotFound",
                "The profile with the specified identifier was not found.");

        public static Error Duplicated =>
            Error.Create("Profile.DuplicateCpf",
                "The specified cpf is already in use.");

        public static Error AlreadyActivated =>
            Error.Create("Profile.AlreadyActivated",
                "The profile has already been activated.");

        public static Error AlreadyInactivated =>
            Error.Create("Profile.AlreadyInactivated",
                "The profile has already been inactivated.");
    }
}
