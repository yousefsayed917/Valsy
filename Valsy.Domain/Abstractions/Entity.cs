using Valsy.Domain.Exceptions;

namespace Valsy.Domain;

public abstract class Entity : Entity<int>
{
}

public abstract class Entity<TId> : AuditableEntity, IEntity<TId>
{
    public TId Id { get; protected set; } = default!;
    private bool IsFetched;

    private int? _requestedHashCode;

    public bool IsTransient()
    {
        return EqualityComparer<TId>.Default.Equals(Id, default!);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Entity<TId>)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        Entity<TId> item = (Entity<TId>)obj;

        if (item.IsTransient() || IsTransient())
        {
            return false;
        }

        return EqualityComparer<TId>.Default.Equals(item.Id, Id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            _requestedHashCode ??= Id!.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        return base.GetHashCode();
    }

    public void SetIntId(int id)
    {
        if (typeof(TId) == typeof(int))
        {
            Id = (TId)(object)id;
        }
    }

    public bool IsIdFetched()
    {
        return IsFetched;
    }

    public void FetchId()
    {
        IsFetched = true;
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
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
