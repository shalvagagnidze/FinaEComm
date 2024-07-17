using DomainLayer.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public Sex Sex { get; set; }
        public ProductSize Size { get; set; }
        public StockStatus Status { get; set; }
        public Condition Condition { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeleteTime { get; set; }
        public bool isDeleted { get; set; }
        public void DeleteProduct()
        {
            isDeleted = true;
        }

    }
}
