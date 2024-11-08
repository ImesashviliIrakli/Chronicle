using Chronicle.Domain.Shared;
using MediatR;

namespace Chronicle.Application.Interfaces;

public interface ICommandQuery : IRequest<Result>, IBaseRequest
{
}
