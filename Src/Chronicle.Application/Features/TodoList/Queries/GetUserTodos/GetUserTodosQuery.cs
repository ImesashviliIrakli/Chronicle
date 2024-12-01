using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.TodoList.Queries.GetUserTodos;

public record GetUserTodosQuery(bool IsCompleted) : ICommandQuery;