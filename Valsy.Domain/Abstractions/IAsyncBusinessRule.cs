namespace Valsy.Domain;

public interface IAsyncBusinessRule
{
    Task<bool> IsBroken();
    string Message { get; }
}
