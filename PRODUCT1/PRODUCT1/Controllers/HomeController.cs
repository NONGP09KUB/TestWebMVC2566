using Microsoft.AspNetCore.Mvc;
using PRODUCT1.Data;
using System.Diagnostics;

namespace PRODUCT1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _productContext;
        public HomeController(ILogger<HomeController> logger, ProductContext productContext)
        {
            _logger = logger;
            _productContext = productContext;
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
            var product = _productContext.Products.ToList();

            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
