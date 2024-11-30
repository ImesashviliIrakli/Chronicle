using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.TodoList.Commands.CreateTodo;

public record CreateTodoCommand(string Title, DateTime DeadLine, string? Description) : ICommandQuery;
