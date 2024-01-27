namespace PRODUCT1.Service.IService
{
    public class ShoppingCartService : IShoppingCartService<ShoppingCart>
    {
        private readonly ProductContext productContext;
        public ShoppingCartService(ProductContext productContext)
        {
            this.productContext = productContext;
        }
        public void DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
        }
        public void IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            // ตัวนี้ก็คือ update เหมือนกัน 
        }
        public void Save()
        {
            productContext.SaveChanges();
        }
        public void Add(ShoppingCart shoppingCart)
        {
            productContext.Add(shoppingCart);
        }
    }
}
