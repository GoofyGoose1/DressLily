namespace Store.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }

        public Order(int orderId, Product product)
        {
            OrderId = orderId;
            Product = product;
            OrderDate = DateTime.Now;
            TotalPrice = product.Price;
        }
    }
}