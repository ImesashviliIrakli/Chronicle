using Chronicle.Domain.Shared;
using MediatR;

namespace Chronicle.Application.Interfaces;

public interface ICommandQueryHandler<in TRequest> : IRequestHandler<TRequest, Result>
    where TRequest : IRequest<Result>
{
}
