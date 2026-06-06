namespace Valsy.Domain.Common.Abstractions;

public interface IAsyncBusinessRule
{
    Task<bool> IsBroken();
    string Message { get; }
}
