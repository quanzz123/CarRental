using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DbRenalCarContext _context;
        public LoginController(DbRenalCarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Account acc)
        {
            if (acc == null) {

                return NotFound();
            }
            //ma hoa mat khau truoc khhi kiem tra
            string pw = Function.MD5Password(acc.Password);
            //kiem tra email co ton tai trong DB
            var check = _context.Accounts.Where(m => (m.Email == acc.Email) || (m.Password == pw) ).FirstOrDefault();
            if (check == null) {
                //hin thi thong bao neu sau mk
                Function._Message = "sai mat khau hoac emai";
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
