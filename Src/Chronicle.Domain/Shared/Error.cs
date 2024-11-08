namespace Chronicle.Domain.Shared;

public class Error
{
    public string Type { get; set; }
    public string Message { get; set; }

    public Error(string type, string message)
    {
        Type = type;
        Message = message;
    }
}
