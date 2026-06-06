using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class InvaildQueryException : AppException
    {
        public List<(string FieldName, string ErrorMessage)> Errors { get; }

        public InvaildQueryException(List<(string, string)> errors)
        {
            Errors = errors;
        }
    }
}
