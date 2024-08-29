using DomainLayer.Entities.Facets;
using DomainLayer.Entities.Products;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class ProductFacetValueModel
    {
        public Guid ProductId { get; set; }
        public Guid FacetValueId { get; set; }
    }
}
