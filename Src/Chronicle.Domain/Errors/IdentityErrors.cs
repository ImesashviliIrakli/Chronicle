using Chronicle.Domain.Shared;

namespace Chronicle.Domain.Errors;

public class IdentityErrors
{
    public static readonly Error LoginFailed = new(
            "BadRequest",
            "Login failed."
            );

    public static readonly Func<string, Error> RegistrationFailed = errors => new(
        "BadRequest",
        $"Registration failed: {errors}"
        );

    public static readonly Error NotFound = new(
        "NotFound",
        "The specified user: could not be found."
        );

    public static readonly Error Forbidden = new(
        "Forbidden",
        "Need to be logged in."
        );
}
