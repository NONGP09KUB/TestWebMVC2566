using Microsoft.AspNetCore.Mvc;
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
