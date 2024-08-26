using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities.Facets;

namespace DomainLayer.Entities.Products
{
    public class ProductFacetValue : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid FacetValueId { get; set; }
        public FacetValue FacetValue { get; set; }
    }
}
