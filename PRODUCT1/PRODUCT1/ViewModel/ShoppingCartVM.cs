using Microsoft.Build.Framework;

namespace PRODUCT1.ViewModel
{
    public class ShoppingCartVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<ShoppingCart> ListCart { get; set; }

        public IFormFile file { get; set; }
    }
}
