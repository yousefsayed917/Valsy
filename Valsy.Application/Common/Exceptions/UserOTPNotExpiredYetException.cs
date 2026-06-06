using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class UserOTPNotExpiredYetException : AppException
    {
        public string MessageError { get; set; }
        public DateTime ExpirationTime { get; set; }
        public UserOTPNotExpiredYetException(string errorMessage, DateTime expirationTime)
        {
            ExpirationTime = expirationTime;
            MessageError = errorMessage;
        }
    }
}
