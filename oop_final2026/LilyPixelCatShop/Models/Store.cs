namespace Store.Models
{
    public class Store
    {
        private List<Product> products;
        private List<Order> orders;
        private double cashRegister;
        private int nextProductId;
        private int nextOrderId;

        public Store()
        {
            products = new List<Product>();
            orders = new List<Order>();
            cashRegister = 0;
            nextProductId = 1;
            nextOrderId = 1;
            LoadDefaultProducts();
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public List<Order> GetOrders()
        {
            return orders;
        }

        public double GetCashRegister()
        {
            return cashRegister;
        }

        public int GetNextProductId()
        {
            int id = nextProductId;
            nextProductId++;
            return id;
        }

        private void LoadDefaultProducts()
        {
            Product shirt = new Shirt(GetNextProductId(), "Cute Pink Shirt", 25, 6, "pinkshirt.PNG", "LilyShirt.PNG", "Small", "Pixel Art", "Cute Pink Shirt.");
            shirt.IsDefaultProduct = true;
            products.Add(shirt);

            Product hat = new Hat(GetNextProductId(), "Propellor Hat", 30, 5, "propellorhat.png", "LilyHat.PNG", "Medium", "Pixel Art", "Propellor Hat.");
            hat.IsDefaultProduct = true;
            products.Add(hat);

            Product overall = new Overall(GetNextProductId(), "Blue Jeans Overall", 45, 4, "overall.png", "LilyOverall.PNG", "Medium", "Pixel Art", "Blue Jeans Overall.");
            overall.IsDefaultProduct = true;
            products.Add(overall);

            Product hoodie = new Hoodie(GetNextProductId(), "Cool Pink Hoodie", 50, 3, "hoodie.png", "LilyHoodie.PNG", "Small", "Pixel Art", "Cool Pink Hoodie.");
            hoodie.IsDefaultProduct = true;
            products.Add(hoodie);

            Product pants = new Pants(GetNextProductId(), "Blue Jeans Pants", 18, 10, "pants.png", "LilyPants.PNG", "Small", "Pixel Art", "Blue Jeans Pants.");
            pants.IsDefaultProduct = true;
            products.Add(pants);

            Product dress = new Dress(GetNextProductId(), "Cute Bunny Dress", 15, 8, "dress.png", "LilyDress.PNG", "Small", "Pixel Art", "Cute Bunny Dress");
            dress.IsDefaultProduct = true;
            products.Add(dress);
        }
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public bool SellProduct(int productId)
        {
            Product product = FindProductById(productId);

            if (product == null || product.Quantity <= 0)
                return false;

            product.Quantity--;
            cashRegister += product.Price;

            Order order = new Order(nextOrderId, product);
            orders.Add(order);
            nextOrderId++;

            return true;
        }

        public bool AddStock(int productId, int amount)
        {
            Product product = FindProductById(productId);

            if (product == null || amount <= 0)
                return false;

            product.Quantity += amount;
            return true;
        }

        public bool DeleteProduct(int productId)
        {
            Product product = FindProductById(productId);

            if (product == null)
                return false;

            if (product.IsDefaultProduct)
                return false;

            products.Remove(product);
            return true;
        }

        public Product FindProductById(int id)
        {
            foreach (Product product in products)
            {
                if (product.Id == id)
                    return product;
            }

            return null;
        }

        public Product GetMostExpensiveProduct()
        {
            if (products.Count == 0)
                return null;

            Product mostExpensive = products[0];

            foreach (Product product in products)
            {
                if (product.Price > mostExpensive.Price)
                    mostExpensive = product;
            }

            return mostExpensive;
        }

        public Product GetCheapestProduct()
        {
            if (products.Count == 0)
                return null;

            Product cheapest = products[0];

            foreach (Product product in products)
            {
                if (product.Price < cheapest.Price)
                    cheapest = product;
            }

            return cheapest;
        }

        public int GetQuantityByCategory(string category)
        {
            int total = 0;

            foreach (Product product in products)
            {
                if (product.GetCategory() == category)
                    total += product.Quantity;
            }

            return total;
        }
    }
}