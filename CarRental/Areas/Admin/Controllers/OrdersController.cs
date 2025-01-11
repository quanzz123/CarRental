using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly DbRenalCarContext _context;
        public OrdersController(DbRenalCarContext context)
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
            var order = _context.CarRentalOrders.OrderByDescending(o => o.OrderId).Include(o=>o.Customer).Include(o=>o.Status).ToList();
            return View(order);
        }
    }
}
