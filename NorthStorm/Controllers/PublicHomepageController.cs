using Microsoft.AspNetCore.Mvc;

namespace NorthStorm.Controllers
{

    public class PublicHomepageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Policy()
        {
            return View();
        }

        public IActionResult Agreement()
        {
            return View();

        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Leave_Services()
        {
            return View();
        }

        public IActionResult Training_Services()
        {
            return View();
        }
        public IActionResult Promotion_Services()
        {
            return View();
        }
        public IActionResult Bonus_Services()
        {
            return View();
        }
        public IActionResult BabaCentral_Entertain()
        {
            return View();
        }
        public IActionResult ArafaCentral_Entertain()
        {
            return View();
        }
        public IActionResult Cinema_Entertain()
        {
            return View();
        }
        public IActionResult Hotel_Entertain()
        {
            return View();
        }
    }
}

