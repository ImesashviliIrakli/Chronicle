using Chronicle.Application.Identity.Commands.Login;
using Chronicle.Application.Identity.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chronicle.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class IdentityController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        var data = await _mediator.Send(loginCommand);

        return CreateResponse(data);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand registerCommand)
    {
        var data = await _mediator.Send(registerCommand);

        return CreateResponse(data);
    }
}
