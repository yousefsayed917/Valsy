namespace Valsy.Domain;

public class AuditableEntity : IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
