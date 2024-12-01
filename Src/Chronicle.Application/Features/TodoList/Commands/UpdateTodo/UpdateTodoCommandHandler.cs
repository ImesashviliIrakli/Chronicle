using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;

namespace Chronicle.Application.Features.TodoList.Commands.UpdateTodo;

public class UpdateTodoCommandHandler(
        IRepository<Todo> repository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
    ) : ICommandQueryHandler<UpdateTodoCommand>
{
    private readonly IRepository<Todo> _repository = repository;
    private readonly IUserContext _userContext = userContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.TodoId);

        if (todo is null)
            return Result.Failure(GlobalStatusCodes.NotFound, TodoErrors.NotFound);

        if (!_userContext.UserId.Equals(todo.UserId))
            return Result.Failure(GlobalStatusCodes.BadRequest, TodoErrors.TodoOwnershipMismatch);

        todo.Edit(request.Title, request.Deadline.ToUniversalTime(), request.Description);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
