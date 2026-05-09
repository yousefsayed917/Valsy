namespace Valsy.Domain.Exceptions;

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
