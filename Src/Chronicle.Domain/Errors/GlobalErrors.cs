using Chronicle.Domain.Shared;

namespace Chronicle.Domain.Errors;

public class GlobalErrors
{
    public static readonly Func<string, Error> SystemFailure = exception => new(
        "SystemFailure",
        exception
        );
}
