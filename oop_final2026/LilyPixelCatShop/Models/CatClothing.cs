namespace Store.Models
{
    public abstract class CatClothing : Product
    {
        private string size;
        private string pixelStyle;
        private string description;

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string PixelStyle
        {
            get { return pixelStyle; }
            set { pixelStyle = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public CatClothing(
            int id,
            string name,
            double price,
            int quantity,
            string imageName,
            string catImageName,
            string size,
            string pixelStyle,
            string description
        ) : base(id, name, price, quantity, imageName, catImageName)
        {
            Size = size;
            PixelStyle = pixelStyle;
            Description = description;
        }

        public override string GetDetails()
        {
            return base.GetDetails() + $" | Size: {Size} | Style: {PixelStyle}";
        }
    }
}