using Chronicle.Domain.Primitives;

namespace Chronicle.Domain.Entities;

public sealed class Todo : Entity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime DeadLine { get; private set; }
    public bool IsCompleted { get; private set; }

    // FOR EFCORE
    public Todo() { }

    public Todo(Guid userId, string title, DateTime deadLine, string? description)
    {
        UserId = userId;
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        DeadLine = deadLine;
    }

    public void Edit(string title, DateTime deadLine, string? description)
    {
        Title = title;
        Description = description;
        DeadLine = deadLine;
    }

    public void ToggleComplete()
    {
        IsCompleted = !IsCompleted;
    }

}
