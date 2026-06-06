using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class DbConcurrencyException : AppException
    {
        public string MessageError { get; set; }
        public DbConcurrencyException()
        {
            MessageError = "This record has been modified by another user. Please refresh and try again.";
        }
    }
}
