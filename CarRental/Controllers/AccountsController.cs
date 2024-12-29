using CarRental.Models;
using CarRental.Utilities;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarRental.Controllers
{
    public class AccountsController : Controller
    {
        private readonly DbRenalCarContext _context;
        public AccountsController(DbRenalCarContext context)
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
            // Lấy ID khách hàng từ session hoặc hệ thống xác thực
            var customerId = Function._AccountId;

            // Truy vấn thông tin khách hàng từ database
            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return RedirectToAction("Index", "Home"); // Redirect nếu không tìm thấy khách hàng
            }

            // Truyền dữ liệu khách hàng sang View
            return View(customer);
            
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
        [HttpGet]
        public IActionResult editAdress()
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            //lấy thông tin khách hàng từ DB
            var id = Function._AccountId;
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if(customer == null)
            {
                return View();
            }
            //truyền thông  tin dếnd view
            var model = new EditAddressVM
            {
                CustomerID = customer.CustomerId,
                Address = customer.Address,
                Phone = customer.PhoneNumber,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult editAdress(EditAddressVM model)
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                var id = Function._AccountId;
                var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                //cập nhật địa chỉ và số điện thoại mới cho khách hàng
                customer.Address = model.Address;
                customer.PhoneNumber = model.Phone;
                _context.SaveChanges();

                return RedirectToAction("Index", "Accounts");
            }
            return View();
        }

    }
}
