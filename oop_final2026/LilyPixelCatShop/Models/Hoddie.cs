// Hoodie.cs

namespace Store.Models
{
    public class Hoodie : CatClothing
    {
        public Hoodie(
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
            return "Hoodie";
        }
    }
}