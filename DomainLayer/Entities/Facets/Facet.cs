using DomainLayer.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Facets
{
    public class Facet : BaseEntity
    {
        public string Name { get; set; }
        public FacetTypeEnum DisplayType { get; set; }
        public bool IsCustom { get; set; }

        public ICollection<FacetValue>? FacetValues { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
