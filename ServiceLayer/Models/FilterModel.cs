using DomainLayer.Common.Enums;

namespace ServiceLayer.Models;

public class FilterModel
{
    public ICollection<Guid>? BrandIds { get; set; }
    public ICollection<Guid>? CategoryIds { get; set; }
    public ICollection<Gender>? Gender { get; set; }
    public ICollection<ProductSize>? ProductSize { get; set; }
    public ICollection<Condition>? Condition { get; set; }
    public StockStatus? StockStatus { get; set; }
    public ICollection<Specification>? Specifications { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

}
