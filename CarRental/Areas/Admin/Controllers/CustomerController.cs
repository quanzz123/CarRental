using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly DbRenalCarContext _context;
        public CustomerController(DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            var c = _context.Customers.OrderBy(b => b.CustomerId).ToList();
            return View(c);
        }
        [HttpGet]
        public IActionResult Details (int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }
            var product = _context.Customers.FirstOrDefault(m => m.CustomerId == id);
            return View(product);
        }
    }
}
