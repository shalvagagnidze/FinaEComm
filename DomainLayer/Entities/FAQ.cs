using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class FAQ : BaseEntity
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFeatured { get; set; }
        public int? OrderNum { get; set; }

    }
}
