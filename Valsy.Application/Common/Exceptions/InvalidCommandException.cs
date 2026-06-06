using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class InvalidCommandException : AppException
    {
        public List<(string FieldName, string ErrorMessage)> Errors { get; }

        public InvalidCommandException(List<(string, string)> errors)
        {
            Errors = errors;
        }
    }
}
