using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRODUCT1.Data;
using PRODUCT1.ViewModel;

namespace PRODUCT1.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly ProductContext _productContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(ProductContext productContext, IWebHostEnvironment webHostEnvironment)
        {
            _productContext = productContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var orders = _productContext.OrderHeaders.ToList();
            return View(orders);
        }

        public IActionResult Details(int id)    
        {
            OrderVM orderVM = new()
            {
                OrderHeader = _productContext.OrderHeaders.Include(x => x.User).FirstOrDefault(x => x.Id == id),
                OrderDetails = _productContext.OrderDetails.Include(x => x.Product).Where(x => x.Id == id)
            };
            orderVM.OrderHeader.PaymentImage = SD.PaymentPath + "\\" + orderVM.OrderHeader?.PaymentImage;
            return View(orderVM);
        }

        [HttpPost]
        //[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderHeader()
        {
            var orderHeaderFromDb =_productContext.OrderHeaders.Find(OrderVM.OrderHeader.Id);

            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
            orderHeaderFromDb.State = OrderVM.OrderHeader.State;
            orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;
            _productContext.OrderHeaders.Update(orderHeaderFromDb);
            _productContext.SaveChanges();

            TempData["Error"] = "Order Header Updated Successfully.";
            return RedirectToAction("Details", "Order", new
            {
                Id = orderHeaderFromDb.Id
            });
        }
        [HttpPost]
        //[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [ValidateAntiForgeryToken]
        public IActionResult StatusOrder(string status)
        {
            var orderHeaderFromDb =  _productContext.OrderHeaders.Find(OrderVM.OrderHeader.Id);
            if (orderHeaderFromDb.OrderStatus == SD.StatusPending)
            {
                orderHeaderFromDb.OrderStatus = status;
                TempData["Success"] = "Status has been updated Succesfully.";
                _productContext.SaveChanges();
            }
            else
            {
             
                TempData["Error"] = "Can't update because status has ended.";
            }
            return RedirectToAction("Details", "Order", new
            {
                Id = OrderVM.OrderHeader.Id
            });
        }

    }
}   
