using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace Chronicle.Application.Features.Identity.Commands.Register;

public class RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IAuthUnitOfWork unitOfWork
    ) : ICommandQueryHandler<RegisterCommand>
{
    private readonly IAuthUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = true,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Password);

            if (!createUserResult.Succeeded)
            {
                transaction.Rollback();
                return Result.Failure(GlobalStatusCodes.SystemFailure, IdentityErrors.RegistrationFailed(BuildErrorMessage(createUserResult.Errors)));
            }

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("UserName", user.UserName),
                new Claim("UserId", user.Id)
            };

            var addClaimsResult = await _userManager.AddClaimsAsync(user, claims);

            if (!addClaimsResult.Succeeded)
            {
                transaction.Rollback();
                return Result.Failure(GlobalStatusCodes.SystemFailure, IdentityErrors.RegistrationFailed(BuildErrorMessage(addClaimsResult.Errors)));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transaction.Commit();

            return Result.Success(new { userId = user.Id });
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new Exception("Registration failed: " + ex.Message);
        }
    }

    private string BuildErrorMessage(IEnumerable<IdentityError> errors)
    {
        var stringBuilder = new StringBuilder();
        foreach (var error in errors)
        {
            stringBuilder.AppendLine($"• {error.Description}");
        }
        return stringBuilder.ToString();
    }
}
