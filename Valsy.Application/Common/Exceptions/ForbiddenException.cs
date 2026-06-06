using Valsy.Domain.Common.Exceptions;

namespace BuildingBlocks.Application.Exceptions
{
    public class ForbiddenException : AppException
    {
        public string MessageError { get; set; }
        public ForbiddenException(string errorMessage)
        {
            MessageError = errorMessage;
        }
    }
}
