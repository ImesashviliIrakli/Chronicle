using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Chronicle.Application.Features.Identity.Commands.GoogleLogin;

public class GoogleLoginCommandHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager
    ) : ICommandQueryHandler<GoogleLoginCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public async Task<Result> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var createUserResult = await _userManager.CreateAsync(user);
            if (!createUserResult.Succeeded)
                return Result.Failure(GlobalStatusCodes.SystemFailure, GlobalErrors.SystemFailure);
        }

        // Here you can add any additional login logic, like issuing a JWT if applicable
        await _signInManager.SignInAsync(user, isPersistent: true);

        var data = new { user.Email, user.FirstName, user.LastName };
        return Result.Success(data);
    }
}
