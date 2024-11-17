namespace Ecommerce.Domain.Entities;

public abstract class BaseEntity<TEntityID>
{
    public TEntityID Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
