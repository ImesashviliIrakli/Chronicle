using Chronicle.Domain.Enums;
using Chronicle.Domain.Shared;
using FluentValidation;
using MediatR;

namespace Chronicle.Application.Behaviors;

public class ValidationPipelineBehavior : IPipelineBehavior<IRequest<Result>, Result>
{
    private readonly IEnumerable<IValidator<IRequest<Result>>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<IRequest<Result>>> validators)
    {
        _validators = validators;
    }

    public async Task<Result> Handle(IRequest<Result> request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return Result.Failure(GlobalStatusCodes.ValidationError, errors);
        }

        return await next();
    }
}
