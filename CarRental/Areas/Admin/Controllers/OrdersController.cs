using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var order = _context.CarRentalOrders
                        .Include(o => o.Customer)
                        .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            var Odstatus = (from o in _context.OrderStatuses
                            select new SelectListItem()
                            {
                                Text = o.StatusDescription,
                                Value = o.StatusId.ToString(),
                            }).ToList();
            Odstatus.Insert(0, new SelectListItem()
            {
                    Text = "--Trạng thái",
                    Value = "0",
            });
            ViewBag.OrderStatus = Odstatus;
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarRentalOrder o)
        {
            if (ModelState.IsValid)
            { // Nạp đơn hàng hiện tại từ database
                var existingOrder = _context.CarRentalOrders
                                            .Include(order => order.Customer)
                                            .FirstOrDefault(order => order.OrderId == o.OrderId);

                if (existingOrder == null)
                {
                    return NotFound();
                }
                existingOrder.StatusId = o.StatusId;

                _context.CarRentalOrders.Update(existingOrder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(o);
        }
    }
}
