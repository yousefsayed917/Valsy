namespace Valsy.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; protected set; }
    public string CreatedBy { get; protected set; } = string.Empty;
    public DateTime? LastModifiedAt { get; protected set; }
    public string? LastModifiedBy { get; protected set; }
    public bool IsDeleted { get; protected set; } = false;

    protected void SetCreationAudit(string createdBy)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    protected void SetModificationAudit(string modifiedBy)
    {
        LastModifiedAt = DateTime.UtcNow;
        LastModifiedBy = modifiedBy;
    }

    public void MarkAsDeleted(string deletedBy)
    {
        IsDeleted = true;
        SetModificationAudit(deletedBy);
    }
}
