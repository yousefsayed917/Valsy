using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Valsy.Application.Common.Abstracts;
using Valsy.Application.Common.Exceptions;

namespace BuildingBlocks.Infrastructure.Validation
{
    public class ValidationQueryHandlerWithResultDecorator<T, TResult> : IRequestHandler<T, TResult>
        where T : QueryBase<TResult>
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly IRequestHandler<T, TResult> _decorated;

        public ValidationQueryHandlerWithResultDecorator(
            IList<IValidator<T>> validators,
            IRequestHandler<T, TResult> decorated)
        {
            _validators = validators;
            _decorated = decorated;
        }

        public Task<TResult> Handle(T query, CancellationToken cancellationToken)
        {
            List<ValidationFailure> errors = _validators
                .Select(v => v.Validate(query))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                throw new InvaildQueryException(errors.Select(x => (x.PropertyName, x.ErrorMessage)).ToList());
            }

            return _decorated.Handle(query, cancellationToken);
        }
    }

}
