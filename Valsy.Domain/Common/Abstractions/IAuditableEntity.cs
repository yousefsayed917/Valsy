namespace Valsy.Domain.Common.Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime LastModifiedAt { get; set; }
    string CreatedBy { get; set; }
    string LastModifiedBy { get; set; }
    bool IsDeleted { get; set; }
}
