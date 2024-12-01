using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.TodoList.Commands.DeleteTodo;

public record DeleteTodoCommand (int TodoId) : ICommandQuery;
