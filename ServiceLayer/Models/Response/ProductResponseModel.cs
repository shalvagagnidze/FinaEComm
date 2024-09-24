using DomainLayer.Common.Enums;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class ProductResponseModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public StockStatus Status { get; set; }
        public Condition Condition { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
        public ICollection<ProductFacetValueResponseModel> ProductFacetValues { get; set; } = new List<ProductFacetValueResponseModel>();
    }
}
