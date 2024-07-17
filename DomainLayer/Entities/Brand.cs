using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Brand : BaseEntity
    {
        public string? Name { get; set; }
        public string? Origin { get; set; }
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DeleteTime { get; set; }
        public bool IsDeleted { get; set; }
        public void DeleteBrand()
        {
            IsDeleted = true;
        }
    }
}
