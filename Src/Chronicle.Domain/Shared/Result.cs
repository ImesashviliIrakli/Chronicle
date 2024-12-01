using Chronicle.Domain.Enums;

namespace Chronicle.Domain.Shared;

public class Result
{
    public GlobalStatusCodes Code { get; set; }
    public object? Data { get; set; }

    protected internal Result(GlobalStatusCodes code, object? data = null)
    {
        Code = code;
        Data = data;
    }

    public static Result Success() => new(GlobalStatusCodes.Success, Array.Empty<object>());

    public static Result Success(object data) => new(GlobalStatusCodes.Success, data);

    public static Result Failure(GlobalStatusCodes code, object data) => new(code, data);
}
