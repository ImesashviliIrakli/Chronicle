using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;

namespace Chronicle.Application.Features.TodoList.Commands.CompleteTodo;

internal sealed class ToggleCompleteTodoCommandHandler(
        IRepository<Todo> repository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
    ): ICommandQueryHandler<ToggleCompleteTodoCommand>
{

    private readonly IRepository<Todo> _repository = repository;
    private readonly IUserContext _userContext = userContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ToggleCompleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.TodoId);

        if (todo is null)
            return Result.Failure(GlobalStatusCodes.NotFound, TodoErrors.NotFound);

        if (!_userContext.UserId.Equals(todo.UserId))
            return Result.Failure(GlobalStatusCodes.BadRequest, TodoErrors.TodoOwnershipMismatch);

        todo.ToggleComplete();

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
