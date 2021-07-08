using System;
using System.Collections.Generic;

#nullable disable

namespace TranShop.Entities
{
    public partial class ProductParame
    {
        public long Id { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public long ProductId { get; set; }
        public long CategoryParameId { get; set; }
        public string Value { get; set; }

        public virtual CategoryParame CategoryParame { get; set; }
        public virtual Product Product { get; set; }
    }
}
