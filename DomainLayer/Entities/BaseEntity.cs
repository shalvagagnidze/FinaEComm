namespace DomainLayer.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity() 
    {
        this.Id = Guid.Empty;
    }
}
