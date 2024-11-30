using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.Identity.Commands.Register;

public record RegisterCommand(string Email, string UserName, string FirstName, string LastName, string Password) : ICommandQuery;
