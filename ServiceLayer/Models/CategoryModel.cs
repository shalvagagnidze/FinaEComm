namespace ServiceLayer.Models;

namespace ServiceLayer.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<FacetModel> Facets { get; set; } = new List<FacetModel>();
        public Guid? ParentId
        {
            get; set;
        }
}
