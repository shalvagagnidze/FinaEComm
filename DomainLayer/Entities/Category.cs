namespace DomainLayer.Entities;

public class Category : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public List<Product>? Products { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; }
    public void DeleteCategory()
    {
        IsDeleted = true;
    }
}
