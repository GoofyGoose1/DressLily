namespace Store.Models
{
    public class Accessory : CatClothing
    {
        public Accessory(
            int id,
            string name,
            double price,
            int quantity,
            string imageName,
            string catImageName,
            string size,
            string pixelStyle,
            string description
        ) : base(id, name, price, quantity, imageName, catImageName, size, pixelStyle, description)
        {
        }

        public override string GetCategory()
        {
            return "Accessory";
        }
    }
}