using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Common.Exceptions;

namespace Valsy.Domain.Common;

public abstract class Entity : Entity<int>
{

}

public abstract class Entity<TId> : AuditableEntity, IEntity<TId>
{
    public TId Id { get; protected set; }
    private bool IsFetched;


    int? _requestedHashCode;

    public bool IsTransient()
    {
        return Id.Equals(default(TId));
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Entity<TId>))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        Entity<TId> item = (Entity<TId>)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return false;//return item == this;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            _requestedHashCode ??= Id.GetHashCode() ^ 31;

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();
    }

    public void SetIntId(int id)
    {
        if (typeof(TId) == typeof(int))
            this.Id = (TId)(object)id;
    }

    public bool IsIdFetched()
    {
        return IsFetched;
    }

    public void FetchId()
    {
        this.IsFetched = true;
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (Equals(left, null))
            return Equals(right, null) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
    protected static async Task CheckRuleAsync(IAsyncBusinessRule rule)
    {
        bool isBroken = await rule.IsBroken();
        if (isBroken)
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
