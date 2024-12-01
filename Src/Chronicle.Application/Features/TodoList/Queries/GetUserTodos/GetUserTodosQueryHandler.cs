using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;

namespace Chronicle.Application.Features.TodoList.Queries.GetUserTodos;

public class GetUserTodosQueryHandler(
    IRepository<Todo> repository,
    IUserContext userContext,
    IUnitOfWork unitOfWork
    ) : ICommandQueryHandler<GetUserTodosQuery>
{
    private readonly IRepository<Todo> _repository = repository;
    private readonly IUserContext _userContext = userContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(GetUserTodosQuery request, CancellationToken cancellationToken)
    {
        if (!_userContext.IsAuthenticated)
            return Result.Failure(GlobalStatusCodes.Forbidden, IdentityErrors.Forbidden);

        var todos = await _repository.GetByUserIdAsync(_userContext.UserId);

        todos = todos.Where(x => x.IsCompleted == request.IsCompleted).ToList();

        return Result.Success(todos);
    }
}
