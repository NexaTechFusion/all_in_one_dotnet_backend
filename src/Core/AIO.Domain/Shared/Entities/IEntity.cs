namespace AIO.Domain.Shared.Entities;

public interface IEntity: ITimeModification
{
    
}
public interface ITimeModification
{
    DateTime? CreatedTime { get; set; }
    DateTime? ModifiedDate { get; set; }
    DateTime? DeletedDate { get; set; }
}