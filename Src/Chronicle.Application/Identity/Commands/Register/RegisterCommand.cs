using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Identity.Commands.Register;

public record RegisterCommand(string Email, string UserName, string FirstName, string LastName, string Password) : ICommandQuery;
