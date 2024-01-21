using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PRODUCT1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ProductContext productContext;
        public CategoryController(ProductContext productContext)
        {
            this.productContext = productContext;
        }
        public IActionResult Index()
        {
            var data = productContext.Categories.ToList();
            return View(data);
        }

        //===============================================================================
        public IActionResult Upcreate(int? id)
        {
            var category = new Category();
            if(id == null || id == 0)
            {
                //
            }
            else
            {
                category = productContext.Categories.Find(id);
                if(category == null)
                { 
                    TempData["Error"] = "Error";
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upcreate(Category category)
        {
            var id = category.Id;
            
            if ( id == 0)
            {
                productContext.Categories.Add(category); 
            }
            else
            {
                productContext.Categories.Update(category);
            }
            productContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        //===============================================================================

        public IActionResult Delete(int? id)
        {
            var category = productContext.Categories.Find(id);
            var  Form = productContext.Products.Any(p => p.CategoryId == category.Id);
            if (Form)
            {
                TempData["Error"] = " มีสินค้าที่ใช้งาน 'หมวดหมู่นี้อยู่' ไม่สามารถลบได้ ";
                return RedirectToAction("Index");
            }

            var cat = productContext.Categories.Find(id);
            if (cat != null)
            {
                productContext.Categories.Remove(cat);
                productContext.SaveChanges();
                TempData["Error"] = "ลบสำเร็จ";
            }
            return RedirectToAction(nameof(Index));

        }
        //===============================================================================

    }
}
