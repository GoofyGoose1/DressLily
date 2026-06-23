namespace Store.Models
{
    public class ShoppingCart
    {
        private List<Product> products;
        public ShoppingCart()
        {
            products = new List<Product>();
        }
        public void AddToCart(Product product)
        {
            products.Add(product);
        }
        public List<Product> GetProducts()
        {
            return products;
        }
        public double GetTotalPrice()
        {
            double total = 0;
            foreach (Product product in products)
            {
                total += product.Price;
            }
            return total;
        }
        public int GetItemsCount()
        {
            return products.Count;
        }
    }
}