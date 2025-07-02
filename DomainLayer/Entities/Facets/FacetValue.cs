using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Facets
{
    public class FacetValue : BaseEntity
    {
        public string? Value { get; set; }
        public Guid FacetId { get; set; }
        public Guid? ParentId { get; set; }

        public Facet? Facet { get; set; }
    }
}
