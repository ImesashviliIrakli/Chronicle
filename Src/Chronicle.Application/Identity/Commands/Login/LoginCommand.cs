using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Identity.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommandQuery;