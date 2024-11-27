using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using CarRental.Utilities;
namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly DbRenalCarContext _context;
        public RegisterController(DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Index(Account acc) 
        {
            if (acc == null) {
                return NotFound();
            }
            //kiem tra su ton tai cua email trong DB
            var check = _context.Accounts.Where(m=> m.Email == acc.Email).FirstOrDefault();
            if (check != null) {
                //hien thi thong bao 
                Function._MessageEmail = "Dupplicate Email";
                return RedirectToAction("Index", "Register");
            }
            //neu khong thi them vao DB
            Function._MessageEmail = string.Empty;
            acc.Password = Function.MD5Password(acc.Password);
            _context.Add(acc);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
