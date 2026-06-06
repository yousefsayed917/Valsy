using FluentValidation;
using MediatR;
using Valsy.Application.Common.Abstracts;
using Valsy.Application.Common.Exceptions;

namespace BuildingBlocks.Infrastructure.Validation
{
    public class ValidationCommandHandlerWithResultDecorator<T, TResult> : IRequestHandler<T, TResult>
        where T : CommandBase<TResult>
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly IRequestHandler<T, TResult> _decorated;

        public ValidationCommandHandlerWithResultDecorator(
            IList<IValidator<T>> validators,
            IRequestHandler<T, TResult> decorated)
        {
            this._validators = validators;
            _decorated = decorated;
        }

        public Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            List<FluentValidation.Results.ValidationFailure> errors = _validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                throw new InvalidCommandException(errors.Select(x => (x.PropertyName, x.ErrorMessage)).ToList());
            }

            return _decorated.Handle(command, cancellationToken);
        }
    }

}
