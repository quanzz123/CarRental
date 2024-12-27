using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public IActionResult Logout()
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            Function._AccountId = 0;
            Function._UserName = string.Empty;
            Function._Email = string.Empty;
            Function._Message = string.Empty;
            Function._MessageEmail = string.Empty;
            return RedirectToAction("Index", "Home");
        }

    }
}
