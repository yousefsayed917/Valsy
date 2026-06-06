using FluentValidation;
using MediatR;
using Valsy.Application.Common.Abstracts;
using Valsy.Application.Common.Exceptions;

namespace BuildingBlocks.Infrastructure.Validation
{
    public class ValidationCommandHandlerDecorator<T> : IRequestHandler<T>
          where T : CommandBase
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly IRequestHandler<T> _decorated;

        public ValidationCommandHandlerDecorator(
            IList<IValidator<T>> validators,
            IRequestHandler<T> decorated)
        {
            this._validators = validators;
            _decorated = decorated;
        }
        public Task Handle(T command, CancellationToken cancellationToken)
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
