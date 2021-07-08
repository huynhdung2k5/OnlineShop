using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranShop.Entities;
using X.PagedList;

namespace TranShop.Controllers
{
    public class CategoriesController : Controller
    {
        TranShopManagementContext db = new TranShopManagementContext();
        public IActionResult Index(int? page, int mode = 0)
        {

            var pageNumber = page ?? 1;
            int pageSize = 6;
            //lay du lieu trong bang Product
            var Cate = db.ProductCategories.ToPagedList(pageNumber, pageSize);
            if (mode == 0)
            {
                return View(Cate);
            }
            else
            {
                return PartialView("_ViewAll", Cate);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddOrEdit(ProductCategory model)
        {

            var Cate = db.ProductCategories;

            if (ModelState.IsValid)
            {
                Cate.Add(model);
                model.CreateDate = DateTime.Now;
                await db.SaveChangesAsync();
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", Cate.ToList())
                });
            }
            return Json(new
            {
                isValid = false,
                html = Helper.RenderRazorViewToString(this, "AddOrEdit", model)
            });
        }

        public IActionResult Status(long Id)
        {
            try
            {
                ProductCategory x = db.ProductCategories.Find(Id);
                x.Status = (x.Status == true) ? false : true;

                x.ModifiedDate = DateTime.Now;
                db.Entry(x).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok(false);
            }
        }
    }
}
