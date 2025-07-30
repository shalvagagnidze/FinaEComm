using DomainLayer.Common.Enums;
namespace DomainLayer.Entities.Products
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public StockStatus Status { get; set; }
        public int StockAmount { get; set; }
        public Condition Condition { get; set; }
        public ICollection<ProductFacetValue>? ProductFacetValues { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeleteTime { get; set; }
        public bool isDeleted { get; set; }
        public bool? IsActive { get; set; } = true;
        public void DeleteProduct()
        {
            isDeleted = true;
        }
    }

}
