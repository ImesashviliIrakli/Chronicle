using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.TodoList.Commands.UpdateTodo;

public record UpdateTodoCommand(int TodoId, string Title, DateTime Deadline, string? Description) : ICommandQuery;
