using Chronicle.Domain.Shared;

namespace Chronicle.Domain.Errors;

public class TodoErrors
{
    public static readonly Error NotFound = new(
        "NotFound",
        "Todo item not found."
        );

    public static readonly Error TodoOwnershipMismatch = new(
        "BadRequest",
        "The specifiec todo item does not belong to the current user."
        );
}
