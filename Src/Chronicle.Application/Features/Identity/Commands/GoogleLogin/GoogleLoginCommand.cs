using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Features.Identity.Commands.GoogleLogin;

public record GoogleLoginCommand(string Email, string FirstName, string LastName) : ICommandQuery;
