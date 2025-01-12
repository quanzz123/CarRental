using CarRental.Models;
using CarRental.Utilities;
using CarRental.ViewModels;
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
        public IActionResult Index(int? filterStatusId)
        {
            // Kiểm tra trạng thái đăng nhập
            if (!Function.IsLogin())
            {
                return RedirectToAction("Index", "Login");
            }

            // Truy vấn đơn hàng
            var orders = _context.CarRentalOrders
                                 .OrderByDescending(o => o.OrderId)
                                 .Include(o => o.Customer)
                                 .Include(o => o.Status)
                                 .AsQueryable();

            // Áp dụng bộ lọc nếu có
            if (filterStatusId.HasValue)
            {
                orders = orders.Where(o => o.StatusId == filterStatusId.Value);
            }
            var status = (from c in _context.OrderStatuses
                           select new SelectListItem()
                           {
                               Text = c.StatusDescription,
                               Value = c.StatusId.ToString(),
                           }).ToList();
           status.Insert(0, new SelectListItem()
            {
                Text = "--select--",
                Value = string.Empty,
            });
            ViewBag.orderstatus = status;
            

            return View(orders.ToList());
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
                                            .Include(order => order.Customer) .Include(c=> c.OrderDetails)
                                            .FirstOrDefault(order => order.OrderId == o.OrderId);

                if (existingOrder == null)
                {
                    return NotFound();
                }
                existingOrder.StatusId = o.StatusId;
                // Nếu trạng thái là "đã hủy" hoặc "đã thanh lý", cập nhật IsActive của xe về true
                if (o.StatusId == 2 || o.StatusId == 5)
                {
                    foreach (var detail in existingOrder.OrderDetails)
                    {
                        var car = _context.Cars.FirstOrDefault(c => c.CarId == detail.CarId);
                        if (car != null)
                        {
                            car.IsActive = true;
                            _context.Cars.Update(car);
                        }
                    }
                }
                _context.CarRentalOrders.Update(existingOrder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(o);
        }
        public IActionResult OrderDetails(int? id)
        {
            var orderItems = _context.OrderDetails
                .Where(od => od.OrderId == id)
                .Join(_context.Cars,
                        od => od.CarId,
                        c => c.CarId,
                        (od, c) => new OrderDetailVM
                        {
                            OrderDetailID = od.OrderId,
                            CarName = c.CarName,
                            Image = c.Image,
                            Quantity = (int)od.Quantity,
                            pickupDate = (DateTime)od.PickupDate,
                            returnDate = (DateTime)od.ReturnDate,

                        })
                .ToList();
            return View(orderItems);
        }
    }
}
