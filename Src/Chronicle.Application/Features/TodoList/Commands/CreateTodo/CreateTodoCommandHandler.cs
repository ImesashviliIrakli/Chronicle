using Chronicle.Application.Interfaces;
using Chronicle.Domain.Entities;
using Chronicle.Domain.Enums;
using Chronicle.Domain.Errors;
using Chronicle.Domain.Repositories;
using Chronicle.Domain.Shared;

namespace Chronicle.Application.Features.TodoList.Commands.CreateTodo;

internal sealed class CreateTodoCommandHandler(IRepository<Todo> repository, IUserContext userContext, IUnitOfWork unitOfWork) : ICommandQueryHandler<CreateTodoCommand>
{
    private readonly IRepository<Todo> _repository = repository;
    private readonly IUserContext _userContext = userContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        if (!_userContext.IsAuthenticated)
            return Result.Failure(GlobalStatusCodes.Forbidden, IdentityErrors.Forbidden);

        var todo = new Todo(_userContext.UserId, request.Title, request.DeadLine.ToUniversalTime(), request.Description);

        await _repository.AddAsync(todo);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
