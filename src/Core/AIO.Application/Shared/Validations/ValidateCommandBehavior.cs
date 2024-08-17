using FluentValidation;
using FluentValidation.Results;
using Mediator;

namespace AIO.Application.Shared.Validations;

public class ValidateCommandBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var context = new ValidationContext<TRequest>(message);
        ValidationResult[] validationResults =
            await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(e => e != null).ToList();

        if (failures.Any())
            throw new ValidationException(failures);


        return await next(message, cancellationToken);
    }
}