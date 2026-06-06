namespace Valsy.Domain.Common.Abstractions;

public interface IEntity<TPrimaryKey>
{
    TPrimaryKey Id { get; }
    bool IsTransient();
    public void SetIntId(int id);
    public bool IsIdFetched();
    public void FetchId();
}
