using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TranShop.Models;
using System.Threading.Tasks;
using TranShop.Entities;
using Microsoft.EntityFrameworkCore;
using TranShop.Models.Product;
using X.PagedList;
using System.IO;

namespace TranShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        TranShopManagementContext db = new TranShopManagementContext();
        public IActionResult IndexProc(int? page, int mode = 0)
        {

            var pageNumber = page ?? 1;
            int pageSize = 6;
            //lay du lieu trong bang Product
            var DbProc = db.Products.
                Join(db.ProductCategories,
                    p => p.Category,
                    c => c.Id,
                    (p, c) => new Product_ProductCategory
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        PromoPrice = p.PromoPrice,
                        Quantity = p.Quantity,
                        CategoryName = c.Name,
                        ModifiedDate = p.ModifiedDate,
                        Status = p.Status,
                        Views = p.Views,
                        IsDeleted = p.IsDeleted
                    }).Where(p => p.IsDeleted != true).ToPagedList(pageNumber, pageSize);
            if (mode == 0)
            {
                return View(DbProc);
            }
            else
            {
                return PartialView("_ViewAll", DbProc);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {

            var DbProc = db.Products.
                Join(db.ProductCategories,
                    p => p.Category,
                    c => c.Id,
                    (p, c) => new Product_ProductCategory
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        PromoPrice = p.PromoPrice,
                        Quantity = p.Quantity,
                        CategoryName = c.Name,
                        ModifiedDate = p.ModifiedDate,
                        Status = p.Status,
                        Views = p.Views
                    });

            if (ModelState.IsValid)
            {
                // Xử lý nhận & lưu file
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var projectPath = Directory.GetCurrentDirectory();
                    var fileName = DateTime.Now.Ticks.ToString() + files[0].FileName;
                    var location = projectPath + "/wwwroot/pics/"+ fileName;

                    var stream = System.IO.File.Create(location);
                    files[0].CopyTo(stream);
                    stream.Dispose();
                    model.ImageProduct = $"/pics/{fileName}";
                }

                db.Products.Add(model);
                await db.SaveChangesAsync();
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", DbProc.ToList())
                });
            }
            return Json(new
            {
                isValid = false,
                html = Helper.RenderRazorViewToString(this, "Create", model)
            });
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            //get cai san pham ra theo cai id sp
            var Proc = db.Products.Find(Id);
            return View(Proc);
        }

        [HttpPost]
        public IActionResult Edit(Product dbproc)
        {
            var data = db.Products.Find(dbproc.Id);
            data.ProductName = dbproc.ProductName;
            data.Description = dbproc.Description;
            data.Price = dbproc.Price;
            data.Quantity = dbproc.Quantity;
            data.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("IndexProc");
        }

        public IActionResult Del(long Id)
        {
            Product x= db.Products.Find(Id);
            x.IsDeleted = true;
            x.ModifiedDate = DateTime.Now;
            db.Entry(x).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexProc");
        }
        public IActionResult _Trash(int? page, int mode = 0)
        {
            var pageNumber = page ?? 1;
            int pageSize = 6;
            //lay du lieu trong bang Product
            var list = db.Products.
                Join(db.ProductCategories,
                    p => p.Category,
                    c => c.Id,
                    (p, c) => new Product_ProductCategory
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        PromoPrice = p.PromoPrice,
                        Quantity = p.Quantity,
                        CategoryName = c.Name,
                        ModifiedDate = p.ModifiedDate,
                        Status = p.Status,
                        Views = p.Views,
                        IsDeleted = p.IsDeleted
                    }).Where(p => p.IsDeleted != false).ToPagedList(pageNumber,pageSize);
            return View(list);
        }

        //xử lý cái ổ khóa
        public IActionResult Status(long Id)
        {
            try
            {
                Product x = db.Products.Find(Id);
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

        //Xóa Thật Sự 
        public IActionResult Delete(long Id)
        {
            Product x = db.Products.Find(Id);
            db.Remove(x);
            db.SaveChanges();
            return RedirectToAction("Trash");
        }

        //Khôi phục rác
        public IActionResult ReTrash(long Id)
        {
            Product x = db.Products.Find(Id);
            x.IsDeleted = false;
            x.ModifiedDate = DateTime.Now;
            db.Entry(x).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("_Trash");
        }
    }
}
