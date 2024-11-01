using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
