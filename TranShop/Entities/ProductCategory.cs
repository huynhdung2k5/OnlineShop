using System;
using System.Collections.Generic;

#nullable disable

namespace TranShop.Entities
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            CategoryParames = new HashSet<CategoryParame>();
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int? TotalProduct { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<CategoryParame> CategoryParames { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
