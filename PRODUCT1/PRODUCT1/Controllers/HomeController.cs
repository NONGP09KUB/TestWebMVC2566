using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRODUCT1.Data;
using PRODUCT1.Service.IService;
using System.Diagnostics;
using System.Security.Claims;

namespace PRODUCT1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _productContext;
        private readonly ShoppingCartService _shoppingCartService;
        public HomeController(ILogger<HomeController> logger, ProductContext productContext, ShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _productContext = productContext;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var product = _productContext.Products.ToList();
            foreach (var item in product)
            {
                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    item.ImageUrl = SD.ProductPath + "\\" + item.ImageUrl;
                }

            }
            return View(product);
        }

        public IActionResult Detail(int productId)
        {
            var Product = _productContext.Products.Include(x => x.Category).FirstOrDefault(x => x.Id.Equals(productId));
            if(Product == null)
            {
                TempData["Error"] = " ไม่มีสินค้า ";
                return RedirectToAction(nameof(Index));
            }
            
            ShoppingCart shoppingCart = new()
            {
                Product = Product,
                Count = 1,
              
            };
            
            return View(shoppingCart);
        }

        //====================================================== เริ่มซื้อตั้งแต่ตรงนี้ =============================
        //====================================================== สำคัญมาก  =============================

        [HttpPost]
        [Authorize] // เหมือนกับเช็ค If ถ้าไม่ได้ Login ไม่สามารถเข้ามาดูได้ || ตรวจสอบสิทธิ์
        //[Authorize(Roles = " Customer,Admin ")] วิธีการระบุ Role ให่สามารถเข้ามาดูได้อย่างเดียว ###
        public IActionResult Detail(ShoppingCart shoppingCart )
        {
            //var ClaimsIdentity  = (ClaimsIdentity) User.Identity; // Id ของ User   
            //var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            shoppingCart.UserId = userId;
            

            var CartformDB = _productContext.ShoppingCarts.FirstOrDefault(x => x.UserId == shoppingCart.UserId && x.ProductId == shoppingCart.ProductId);
            if(CartformDB == null)
            {
                _shoppingCartService.Add(shoppingCart);
            }
            else
            {
                _shoppingCartService.IncrementCount(CartformDB, shoppingCart.Count);
            }
            _shoppingCartService.Save();

            return RedirectToAction(nameof(Index));
        }
        //====================================================== เริ่มซื้อตั้งแต่ตรงนี้ =============================

    }
}
