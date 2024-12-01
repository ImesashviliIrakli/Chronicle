using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.TodoList.Commands.CompleteTodo;

public record ToggleCompleteTodoCommand(int TodoId) : ICommandQuery; 
