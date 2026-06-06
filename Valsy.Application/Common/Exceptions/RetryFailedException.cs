using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class RetryFailedException : AppException
    {
        public string MessageError { get; set; }
        public RetryFailedException()
        {
            MessageError = "Internet Connection Error Try Again.";
        }
    }
}
