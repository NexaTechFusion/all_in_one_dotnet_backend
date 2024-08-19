namespace AIO.Domain.Shared.Entities;

public abstract class BaseEntity<TKey> : IEntity
{
    public required TKey Id { get; set; }

    public DateTime? CreatedTime { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    
}
public abstract class BaseEntity : BaseEntity<int>
{
}