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
    public class ProductFacetValueResponseModel
    {
        public string? FacetName { get; set; }
        public string? FacetValue { get; set; }
    }
}