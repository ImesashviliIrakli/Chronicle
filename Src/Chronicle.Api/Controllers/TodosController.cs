using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chronicle.Api.Controllers;

[Route("Api/[controller]/[action]")]
[ApiController]
public class TodosController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    
}
