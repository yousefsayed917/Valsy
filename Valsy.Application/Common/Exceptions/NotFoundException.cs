using Valsy.Domain.Common.Exceptions;

namespace Valsy.Application.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public List<(string FieldName, string ErrorMessage)> Errors { get; }
        public string MessageError { get; set; }

        public NotFoundException(List<(string, string)> errors)
        {
            Errors = errors;

        }
        public NotFoundException(string messageError, List<(string, string)> errors = null)
        {
            MessageError = messageError;
            Errors = errors;

        }
    }
}
