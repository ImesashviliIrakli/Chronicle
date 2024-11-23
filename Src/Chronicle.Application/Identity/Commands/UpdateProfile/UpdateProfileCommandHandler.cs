using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Chronicle.Application.Identity.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(
        UserManager<ApplicationUser> userManager,
        IAuthUnitOfWork unitOfWork
    ) : ICommandQueryHandler<UpdateProfileCommand>
{

    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IAuthUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
            return Result.Failure(GlobalStatusCodes.NotFound, IdentityErrors.NotFound);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UserName = request.UserName;
        user.PhoneNumber = request.PhoneNumber;

        await _userManager.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
