using System;
using System.Collections.Generic;

#nullable disable

namespace TranShop.Entities
{
    public partial class CategoryParame
    {
        public CategoryParame()
        {
            ProductParames = new HashSet<ProductParame>();
        }

        public long Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<ProductParame> ProductParames { get; set; }
    }
}
