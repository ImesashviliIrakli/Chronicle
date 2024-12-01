using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;

namespace Chronicle.Application.Features.TodoList.Queries.GetTodoById;

public class GetTodoByIdQueryHandler(
        IRepository<Todo> repository,
        IUserContext userContext
    ) : ICommandQueryHandler<GetTodoByIdQuery>
{
    private readonly IRepository<Todo> _repository = repository;
    private readonly IUserContext _userContext = userContext;
    public async Task<Result> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.TodoId);

        if (todo is null)
            return Result.Failure(GlobalStatusCodes.NotFound, TodoErrors.NotFound);

        if (!_userContext.UserId.Equals(todo.UserId))
            return Result.Failure(GlobalStatusCodes.BadRequest, TodoErrors.TodoOwnershipMismatch);

        return Result.Success(todo);
    }
}
