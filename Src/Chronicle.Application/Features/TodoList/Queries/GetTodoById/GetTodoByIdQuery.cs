using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.TodoList.Queries.GetTodoById;

public record GetTodoByIdQuery (int TodoId) : ICommandQuery;
