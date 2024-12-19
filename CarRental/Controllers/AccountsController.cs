using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
