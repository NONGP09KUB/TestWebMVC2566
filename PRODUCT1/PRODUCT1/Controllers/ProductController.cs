using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRODUCT1.Migrations;
using PRODUCT1.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PRODUCT1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ProductContext productContext;

        public ProductController(ProductContext productContext, IWebHostEnvironment webHostEnvironment)
        {
            this.productContext = productContext;
            this.webHostEnvironment = webHostEnvironment;
        }
        //===========================================================================================

        public IActionResult Index()
        {

            var product = productContext.Products.Include(x => x.Category).ToList();
            foreach (var item in product)
            {
                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    item.ImageUrl = SD.ProductPath + "\\" + item.ImageUrl;
                }

            }
            return View(product);
            //.OrderByDescending(x => x.Id) เอาไว้เรียง
        }
        //===========================================================================================

        public IActionResult Upcreate(int? id)
        {
            var productVM = new ProductVM()
            {
                Product = new()
                {
                    Name = "TEst",
                    Description = "123",
                    Price = 123
                },
                CategoryList = productContext.Categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                })
            };
            // ต้องใช้ && เท่านั้นถ้าใช้ || มันอาจจะทำงาน
            if (id != null && id != 0)
            {
                //update
                productVM.Product = productContext.Products.Find(id);
                if (productVM.Product == null)
                {
                    TempData["Error"] = "ไม่มีข้อมูล";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upcreate(ProductVM productVM)
        {
            if (!ModelState.IsValid) { return View(productVM); }
            //=================================== รูปภาพ ===============================================
            string wwwRootPath = webHostEnvironment.WebRootPath;
            var file = productVM.file;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var uploads = wwwRootPath + SD.ProductPath;
                // ของเก่า Path.Combine(wwwRootPath, @"images\products");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                //กรณีมีรูปภาพเดิมตอ้งลบทิ้งก่อน
                if (productVM.Product.ImageUrl != null)
                {
                    // wwwRootPath\images\product\ชื่องาน 
                    var oldImagePath = Path.Combine(uploads, productVM.Product.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                //บันทึกรุปภาพใหม่
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                productVM.Product.ImageUrl = fileName + extension;
                //@"\images\products\" ของเก่า ต่อท้าย =
            }
            //================================== รูปภาพ ================================================

            var id = productVM.Product.Id;

            if (id != null)
            {
                productContext.Products.Update(productVM.Product);

                TempData["Error"] = "อัพเดตข้อมูลเสร็จสิ้น";

            }
            else
            {
                productContext.Products.Add(productVM.Product);
                TempData["Error"] = "เพิ่มข้อมูลสำเร็จ";
            }
            productContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var data = productContext.Products.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath,data.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            productContext.Products.Remove(data);
            productContext.SaveChanges();
            TempData["success"] = "Delete Successful";
            return RedirectToAction(nameof(Index));
        }
    }
}
