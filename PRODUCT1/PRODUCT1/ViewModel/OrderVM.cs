using PRODUCT1.Models;

namespace PRODUCT1.ViewModel
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
