using CarRental.Models;
using CarRental.Utilities;
using CarRental.ViewModels;
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
        [Area("Admin")]

        [HttpPost]
        public IActionResult Index(AdminAccountVM model)
        {
            if (model == null) {

                return NotFound();
            }
            //ma hoa mat khau truoc khhi kiem tra
            string pw = Function.MD5Password(model.Password);
            //kiem tra email co ton tai trong DB
            var check = _context.Accounts.Where(m => m.Email == model.Email && m.Password == pw).FirstOrDefault();

            if (check == null) {
                //hin thi thong bao neu sau mk
                Function._Message = "sai mat khau hoac emai";
                return RedirectToAction("Index", "Login");
            }
            // vao trang admin neu dung usernam va pasw
            Function._Message = string.Empty;
            Function._AccountId = check.AccountId;
            Function._UserName  = string.IsNullOrEmpty(check.Username) ? string.Empty : check.Username; 
            Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            return RedirectToAction("Index", "Home");
        }
    }
}
