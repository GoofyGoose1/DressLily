using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Products()
        {
            Store.Models.Store store = HomeController.GetStore();
            return View(store.GetProducts());
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(string name, double price, int quantity, string category, string size, string description, int scrollY = 0)
        {
            Store.Models.Store store = HomeController.GetStore();

            int id = store.GetNextProductId();
            Product product;

            if (category == "Hat")
                product = new Hat(id, name, price, quantity, "", "LilyHat.PNG", size, "Pixel Art", description);
            else if (category == "Dress")
                product = new Dress(id, name, price, quantity, "", "LilyDress.PNG", size, "Pixel Art", description);
            else if (category == "Accessory")
                product = new Accessory(id, name, price, quantity, "", "LilyRegular.PNG", size, "Pixel Art", description);
            else if (category == "Pants")
                product = new Pants(id, name, price, quantity, "", "LilyPants.PNG", size, "Pixel Art", description);
            else if (category == "Hoodie")
                product = new Hoodie(id, name, price, quantity, "", "LilyHoodie.PNG", size, "Pixel Art", description);
            else if (category == "Overall")
                product = new Overall(id, name, price, quantity, "", "LilyOverall.PNG", size, "Pixel Art", description);
            else if (category == "Shirt")
                product = new Shirt(id, name, price, quantity, "", "LilyShirt.PNG", size, "Pixel Art", description);
            else
            {
                TempData["Message"] = "Invalid product category.";
                return RedirectToAction("AddProduct", scrollY > 0 ? new { scrollY } : null);
            }

            product.IsDefaultProduct = false;
            store.AddProduct(product);

            TempData["Message"] = "Product added successfully!";
            return RedirectToAction("Products", scrollY > 0 ? new { scrollY } : null);
        }

        [HttpPost]
        public IActionResult Buy(int id, int scrollY = 0)
        {
            Store.Models.Store store = HomeController.GetStore();
            bool success = store.SellProduct(id);

            TempData["Message"] = success
                ? "Purchase completed successfully!"
                : "This product is out of stock.";

            return RedirectToAction("Products", scrollY > 0 ? new { scrollY } : null);
        }

        [HttpPost]
        public IActionResult AddStock(int id, int amount, int scrollY = 0)
        {
            Store.Models.Store store = HomeController.GetStore();
            bool success = store.AddStock(id, amount);

            TempData["Message"] = success
                ? "Stock added successfully!"
                : "Could not add stock.";

            return RedirectToAction("Products", scrollY > 0 ? new { scrollY } : null);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id, int scrollY = 0)
        {
            Store.Models.Store store = HomeController.GetStore();
            bool success = store.DeleteProduct(id);

            TempData["Message"] = success
                ? "Product deleted successfully!"
                : "You can only delete products you added.";

            return RedirectToAction("Products", scrollY > 0 ? new { scrollY } : null);
        }

        public IActionResult Report()
        {
            Store.Models.Store store = HomeController.GetStore();
            return View(store);
        }
    }
}
