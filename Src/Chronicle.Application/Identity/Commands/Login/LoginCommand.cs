using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Identity.Commands.Login;

public record LoginCommand(string email, string password) : ICommandQuery;