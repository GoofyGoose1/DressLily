namespace Store.Models
{
    public abstract class Product
    {
        private int id;
        private string name;
        private double price;
        private int quantity;
        private string imageName;
        private string catImageName;
        public bool IsDefaultProduct { get; set; }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                    price = value;
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value >= 0)
                    quantity = value;
            }
        }

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        public string CatImageName
        {
            get { return catImageName; }
            set { catImageName = value; }
        }

        public Product(int id, string name, double price, int quantity, string imageName, string catImageName)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageName = imageName;
            CatImageName = catImageName;
        }

        public abstract string GetCategory();

        public virtual string GetDetails()
        {
            return $"{Name} | {GetCategory()} | ${Price} | Stock: {Quantity}";
        }
    }
}