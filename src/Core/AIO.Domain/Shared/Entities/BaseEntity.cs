namespace AIO.Domain.Shared.Entities;

public abstract class BaseEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public override bool Equals(object obj)
    {
        if (obj is not BaseEntity<TKey> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return GetType() == other.GetType() && Id.Equals(other.Id);
    }
    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}

public abstract class BaseEntity : BaseEntity<int>
{
}