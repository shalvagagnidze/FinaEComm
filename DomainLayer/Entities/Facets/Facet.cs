using DomainLayer.Common.Enums;

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