using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.Identity.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommandQuery;