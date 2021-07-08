using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranShop.Entities;
using TranShop.Models.Product;

namespace TranShop.ViewComponents
{
    public class SelectCategory : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TranShopManagementContext db = new TranShopManagementContext();
            List<ProductCategory> CategoryList = new List<ProductCategory>();
            CategoryList = await (from ProductCategory in db.ProductCategories
                            select ProductCategory).ToListAsync();

            CategoryList.Insert(0, new ProductCategory { Id = 0, Name = "--Chọn danh mục--" });

            return View(CategoryList);
        }
    }
}
