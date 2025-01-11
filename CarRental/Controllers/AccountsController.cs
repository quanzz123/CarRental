using CarRental.Models;
using CarRental.Utilities;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

                return RedirectToAction("Index", "Login", new { returnUrl = HttpContext.Request.Path });
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
            ViewBag.Order = _context.CarRentalOrders.Include(i => i.Status).Where(p => p.CustomerId == customerId).ToList();
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
        public IActionResult AccountDetails()
        {
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            var customerId = Function._AccountId;
            var customer = _context.Customers.FirstOrDefault(p => p.CustomerId == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult AccountDetails(Customer c)
        {
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                var customerId = Function._AccountId;
                var customer = _context.Customers.FirstOrDefault(p => p.CustomerId == customerId);

                if (customer == null)
                {
                    return NotFound();
                }

                customer.Name = c.Name;
                if (c.Email != customer.Email)
                {
                    // Kiểm tra xem email mới có tồn tại trong cơ sở dữ liệu không
                    var emailExists = _context.Customers.Any(p => p.Email == c.Email);
                    if (emailExists)
                    {
                        Function._MessageEmail = "Email đã tồn tại";
                        return View(customer); // Trả về View với dữ liệu để người dùng chỉnh sửa
                    }
                    else
                    {
                        customer.Email = c.Email;
                        Function._MessageEmail = string.Empty;
                    }
                }
                Function._MessageEmail = string.Empty;

                customer.Password = Function.MD5Password(c.Password);

                // Xử lý ảnh đại diện
                /*if (img != null && img.Length > 0)
                {
                    // Gọi hàm UploadImage để lưu ảnh và lấy tên file
                    var uploadedImagePath = Function.UploadImage(img, "Customer");
                    if (!string.IsNullOrEmpty(uploadedImagePath))
                    {
                        customer.Avartar = uploadedImagePath; // Lưu đường dẫn hoặc tên file vào thuộc tính Avartar
                    }
                }*/
                
                _context.SaveChanges();
                Function._UserName = customer.Name;
                return RedirectToAction("Index", "Accounts");

            }


            return View(c);
        }
        [HttpGet]
        public IActionResult licenseDetails()
        {
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            var customerId = Function._AccountId;
            var customer = _context.Customers.FirstOrDefault(p => p.CustomerId == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
            
        }
        [HttpPost]
        public IActionResult licenseDetails(Customer c, IFormFile? img)
        {
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                var customerId = Function._AccountId;
                var customer = _context.Customers.FirstOrDefault(p => p.CustomerId == customerId);

                if (customer == null)
                {
                    return NotFound();
                }

                customer.FullName = c.FullName;
                customer.DateofBirth = c.DateofBirth;
                customer.LicenseNumber  = c.LicenseNumber;
             



                // Xử lý ảnh đại diện
                if (img != null && img.Length > 0)
                {
                    // Gọi hàm UploadImage để lưu ảnh và lấy tên file
                    var uploadedImagePath = Function.UploadImage(img, "license");
                    if (!string.IsNullOrEmpty(uploadedImagePath))
                    {
                        customer.LicenseImage = uploadedImagePath; // Lưu đường dẫn hoặc tên file vào thuộc tính Avartar
                    }
                } else
                {
                    customer.LicenseImage = customer.LicenseImage;
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "Accounts");
            }
            return View(c);
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
            if (customer == null)
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
                Function._address = customer.Address;
                Function._Phone = customer.PhoneNumber;
                return RedirectToAction("Index", "Accounts");
            }

            return View();
        }
        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            var order = _context.CarRentalOrders.Where(p => p.OrderId == id);
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
