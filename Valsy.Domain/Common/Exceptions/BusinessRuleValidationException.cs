using Valsy.Domain.Common.Abstractions;

namespace Valsy.Domain.Common.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(brokenRule.Message)
    {
    }

    public BusinessRuleValidationException(IAsyncBusinessRule brokenRule)
        : base(brokenRule.Message)
    {
    }
}
