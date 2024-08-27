using DomainLayer.Common.Enums;

namespace DomainLayer.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public Gender Gender { get; set; }
    public List<ProductSize>? Size { get; set; }
    public StockStatus Status { get; set; }
    public Condition Condition { get; set; }
    public List<Specification>? Specifications { get; set; }
    public string? Description { get; set; }
    public List<string>? Images { get; set; }
    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DeleteTime { get; set; }
    public bool isDeleted { get; set; }
    public void DeleteProduct()
    {
        isDeleted = true;
    }

}
