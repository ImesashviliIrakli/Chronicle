using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;

namespace Chronicle.Application.Features.TodoList.Commands.DeleteTodo;

public class DeleteTodoCommandHandler(
        IRepository<Todo> repository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
    ) : ICommandQueryHandler<DeleteTodoCommand>
{

    private readonly IRepository<Todo> _repository = repository;
    private readonly IUserContext _userContext = userContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.TodoId);

        if (todo is null)
            return Result.Failure(GlobalStatusCodes.NotFound, TodoErrors.NotFound);

        if (!_userContext.UserId.Equals(todo.UserId))
            return Result.Failure(GlobalStatusCodes.BadRequest, TodoErrors.TodoOwnershipMismatch);

        _repository.Delete(todo);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
