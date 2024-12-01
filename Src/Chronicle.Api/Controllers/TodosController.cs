using Chronicle.Application.Features.TodoList.Commands.CompleteTodo;
using Chronicle.Application.Features.TodoList.Commands.CreateTodo;
using Chronicle.Application.Features.TodoList.Commands.DeleteTodo;
using Chronicle.Application.Features.TodoList.Commands.UpdateTodo;
using Chronicle.Application.Features.TodoList.Queries.GetUserTodos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chronicle.Api.Controllers;

[Route("Api/[controller]")]
[ApiController]
[Authorize]
public class TodosController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    #region Queries
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]bool isCompleted)
    {
        var query = new GetUserTodosQuery(isCompleted);

        var data = await _mediator.Send(query);

        return CreateResponse(data);
    }
    #endregion

    #region Commands

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTodoCommand command)
    {
        var data = await _mediator.Send(command);

        return CreateResponse(data);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTodoCommand command)
    {
        var data = await _mediator.Send(command);
        
        return CreateResponse(data);
    }

    [HttpPatch("{todoId:int}")]
    public async Task<IActionResult> Complete(int todoId)
    {
        var command = new ToggleCompleteTodoCommand(todoId);

        var data = await _mediator.Send(command);

        return CreateResponse(data);
    }

    [HttpDelete("{todoId:int}")]
    public async Task<IActionResult> Delete(int todoId)
    {
        var command = new DeleteTodoCommand(todoId);

        var data = await _mediator.Send(command);

        return CreateResponse(data);
    }
    #endregion
}
