using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [Area("Admin")]

        public IActionResult Logout() {
            Function._AccountId = 0;
            Function._UserName = string.Empty;
            Function._Email = string.Empty;

            Function._Message = string.Empty;

            Function._MessageEmail = string.Empty;
            return RedirectToAction("Index", "Home");

        }
    }
}
