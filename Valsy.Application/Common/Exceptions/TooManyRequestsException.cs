using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class TooManyRequestsException : AppException
    {
        public TimeSpan? RetryAfter { get; }
        public TooManyRequestsException(TimeSpan? retryAfter = null, string? message = null)
            : base(retryAfter == null ? message : $"Too many requests. Retry after {retryAfter.Value.TotalSeconds} seconds.")
        {
            RetryAfter = retryAfter;
        }
    }
}
