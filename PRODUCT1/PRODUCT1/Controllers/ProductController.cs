using Microsoft.AspNetCore.Mvc;

namespace PRODUCT1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext productContext;

        public ProductController(ProductContext productContext)
        {
            this.productContext = productContext;
        }

        public IActionResult Index()
        {
            var product = productContext.Products.Include(x => x.Category).ToList();
            return View(product);
            
        }
    }
}
