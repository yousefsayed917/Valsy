using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public string MessageError { get; set; }
        public UnauthorizedException(string messageError)
        {
            MessageError = messageError;
        }
    }
}
