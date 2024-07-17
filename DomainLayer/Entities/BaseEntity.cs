using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        protected BaseEntity() 
        {
            this.Id = Guid.Empty;
        }
    }
}
