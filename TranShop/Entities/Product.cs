using System;
using System.Collections.Generic;

#nullable disable

namespace TranShop.Entities
{
    public partial class Product
    {
        public Product()
        {
            ProductParames = new HashSet<ProductParame>();
        }

        public long Id { get; set; }
        public long? Category { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? PromoPrice { get; set; }
        public long? Quantity { get; set; }
        public long? QuantitySold { get; set; }
        public long? Views { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DisplayOrder { get; set; }
        public string Metatitle { get; set; }
        public bool? Status { get; set; }
        public bool? OnTop { get; set; }
        public bool? ShowOnHome { get; set; }
        public string ImageProduct { get; set; }

        public virtual ProductCategory CategoryNavigation { get; set; }
        public virtual ICollection<ProductParame> ProductParames { get; set; }
    }
}
