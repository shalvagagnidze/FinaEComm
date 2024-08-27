using DomainLayer.Common.Enums;

namespace ServiceLayer.Models;

public class ProductModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public Gender Gender { get; set; }
    public List<ProductSize>? Size { get; set; }
    public StockStatus Status { get; set; }
    public Condition Condition { get; set; }
    public List<Specification>? Specifications { get; set; }
    public string? Description { get; set; }
    public List<string>? Images{ get; set; }

}
