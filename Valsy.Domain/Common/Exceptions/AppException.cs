using System.Runtime.Serialization;
using Valsy.Domain.Common.Enums;

namespace Valsy.Domain.Common.Exceptions
{
    public class AppException : Exception
    {
        public AppException()
        {
            ErrorId = Guid.NewGuid();
        }

        public AppException(string message, BusinessErrorCode? businessErrorCode = null) : base(message)
        {
            ErrorId = Guid.NewGuid();
            BusinessErrorCode = businessErrorCode;
        }
        public AppException(Exception exception) : base(exception.Message, exception)
        {
            ErrorId = Guid.NewGuid();
        }
        public AppException(string message, Exception exception) : base(message, exception)
        {
            ErrorId = Guid.NewGuid();
        }
        public AppException(SerializationInfo serializationInfo, StreamingContext context)
        {

        }
        public Guid UserId { get; set; }
        public Guid ErrorId { get; }
        public Guid TransactionId { get; set; }
        public string TransactionName { get; set; }
        public BusinessErrorCode? BusinessErrorCode { get; set; }

    }
}
