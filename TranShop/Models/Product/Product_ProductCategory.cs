using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranShop.Models.Product
{
    public class Product_ProductCategory

    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Không được bỏ trống")]
        public long? Category { get; set; }
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int? PromoPrice { get; set; }
       
        [Range(1, 1000, ErrorMessage = "Số lượng không được nhỏ hơn 1")]
        public long? Quantity { get; set; }
        public long? Views { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Status { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
