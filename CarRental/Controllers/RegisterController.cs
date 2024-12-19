using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DbRenalCarContext _context;
        public RegisterController (DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Customer c)
        {
            if (c == null)
            {
                return NotFound();
            }
            //kiểm tra sự tồn tại trong DB
            var check = _context.Customers.Where(m=> m.Email == c.Email).FirstOrDefault();
            if (check != null)
            {
                //hiên thị thông báo
                Function._MessageEmail = "Tài khoản tồn tại";
                return RedirectToAction("Index", "Register");
            }

            //nếu không tồn tại thì thêm vào csdl
            Function._MessageEmail = string.Empty;
            c.Password = Function.MD5Password(c.Password);
            _context.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
 