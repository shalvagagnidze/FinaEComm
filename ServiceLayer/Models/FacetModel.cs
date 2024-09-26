using DomainLayer.Common.Enums;
using DomainLayer.Entities.Facets;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class FacetModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public FacetTypeEnum DisplayType { get; set; }
        public bool IsCustom { get; set; }

        //category list
        public List<Guid>? CategoryIds { get; set; }
        public List<FacetValueModel>? FacetValues { get; set; }
    }
}
