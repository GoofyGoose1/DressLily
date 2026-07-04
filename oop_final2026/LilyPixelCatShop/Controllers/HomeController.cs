using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        private static Store.Models.Store store = new Store.Models.Store();

        public IActionResult Index()
        {
            return View(store.GetProducts());
        }

        public IActionResult AboutLily()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(int id, int scrollY = 0)
        {
            bool success = store.SellProduct(id);

            if (success)
                TempData["Message"] = "Purchase completed successfully!";
            else
                TempData["Message"] = "This product is out of stock.";

            return RedirectToAction("Index", scrollY > 0 ? new { scrollY } : null);
        }

        public static Store.Models.Store GetStore()
        {
            return store;
        }
    }
}
