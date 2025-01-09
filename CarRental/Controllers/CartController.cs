using CarRental.Extensions;
using CarRental.Models;
using CarRental.Utilities;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    public class CartController : Controller
    {
        private readonly DbRenalCarContext _context;
        public CartController(DbRenalCarContext context)
        {
            _context = context;
        }
        string CART_KEY = "MYCART";
        public List<CartItemsVM> CART => HttpContext.Session.Get<List<CartItemsVM>>(CART_KEY) ?? new List<CartItemsVM> ();
        public IActionResult Index()
        {
            
            return View(CART);
        }
        [HttpPost]
        public IActionResult AddToCart(int id, DateTime pickupDate, DateTime returnDate, int quantity = 1, string type = "Normal")
        {

            var cart = CART;
            // lấy thông tin sản phảm từ DB
            var product = _context.Cars.FirstOrDefault(p => p.CarId == id);
            // Kiểm tra logic ngày tháng
            if (returnDate <= pickupDate)
            {
                return Json(new { success = false, message = "Ngày trả xe phải lớn hơn ngày nhận xe." });
            }

            // Xử lý thêm sản phẩm vào giỏ hàng
            int rentalDays = (returnDate - pickupDate).Days;
            if (product == null) {

                return NotFound();// nếu không tìm thấy sp
            }
            decimal finalPrice;
            if (product.SalePrice > 0)
            {
                finalPrice = product.SalePrice ?? 0;
            } else 
            {
                finalPrice = product.Price ?? 0;
            }
            // kiểm tra xem có sản phẩm tồn tai trong giỏ hàng hay chưa
            var CarItem = cart.FirstOrDefault(p => p.CartId == id);
            if (CarItem == null)
            {
                // nếu chưa có sản phẩm thì khởi tạo sản phẩm mới
                CarItem = new CartItemsVM
                {
                    CartId = product.CarId,
                    CarName = product.CarName,
                    Image = product.Image,
                    pickupDate = pickupDate,
                    returnDate = returnDate,
                    Price = finalPrice * rentalDays,
                    Quantity = quantity,
                    
                };
                cart.Add(CarItem);
               
            } else
            {
                // nếu đãn có thì tăng số lượng len
                CarItem.Quantity += quantity;
                if (CarItem.pickupDate != pickupDate || CarItem.returnDate != returnDate)
                {
                    CarItem.pickupDate = pickupDate;
                    CarItem.returnDate = returnDate;

                    // Tính lại số ngày thuê
                    int newRentalDays = (CarItem.returnDate - CarItem.pickupDate).Days;

                    // Cập nhật lại giá thuê
                    CarItem.Price = finalPrice * newRentalDays * CarItem.Quantity;
                }
            }
            HttpContext.Session.Set(CART_KEY, cart);

            if (type == "ajax") 
            {
               return Json( new {
                    quantity = cart.Sum(p => p.Quantity)
                });
            }
            ViewBag.MiniCart = cart;
            return RedirectToAction("Index");
        }
        public IActionResult CheckoutSummary()
        {
            var cart = CART; // Lấy giỏ hàng từ Session
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart"); // Chuyển hướng nếu giỏ hàng trống
            }

            // Tính tổng tiền thuê xe
            decimal total = cart.Sum(item => item.Price);
           

            decimal deposit = total * 0.3m; // Tiền đặt cọc 30%
            decimal payment = total - deposit; // Số tiền còn lại khi nhận xe

            // Tạo ViewModel
            var model = new CartVM
            {
                Total = total,
                Deposit = deposit,
                Payment = payment
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateCart(List<CartUpdateVM> updatedCart)
        {
            // Giả sử bạn có giỏ hàng trong session
            var cart = CART; // Lấy giỏ hàng từ session hoặc cơ sở dữ liệu

            foreach (var updatedItem in updatedCart)
            {
                var cartItem = cart.FirstOrDefault(item => item.CartId == updatedItem.CartId);
                if (cartItem != null)
                {
                    // Cập nhật số lượng và tính lại giá
                    cartItem.Quantity = updatedItem.Quantity;

                    // Tính lại giá thuê xe dựa trên số ngày thuê và số lượng
                    int rentalDays = (cartItem.returnDate - cartItem.pickupDate).Days;
                    cartItem.Price = cartItem.Price * rentalDays * cartItem.Quantity;
                }
            }

            // Cập nhật tổng giá trị giỏ hàng
            /*cart.UpdateTotalPrice();*/

            // Lưu giỏ hàng lại vào session
            HttpContext.Session.Set(CART_KEY, cart);

            // Trả về tổng giá trị giỏ hàng cho client
            return Json(new { totalPrice = cart.Sum(p=>p.PriceTotal) });
        }

        public IActionResult removeCart(int id)
        {
            var cart = CART;
            var items = cart.FirstOrDefault(p => p.CartId == id);
            if(items != null)
            {
                cart.Remove(items);
                HttpContext.Session.Set(CART_KEY, cart);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login", new { ReturnUrl = Url.Action("Checkout", "Cart") });
            }
            var cart = CART; // Lấy giỏ hàng từ Session
            if(cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart"); // Chuyển hướng nếu giỏ hàng trống
            }
            // Tính tổng tiền thuê xe
            decimal total = cart.Sum(item => item.Price);


            decimal deposit = total * 0.3m; // Tiền đặt cọc 30%
            decimal payment = total - deposit;  // Số tiền còn lại khi nhận xe

            // Tạo ViewModel
            var model = new CartVM
            {
                Total = total,
                Deposit = deposit,
                Payment = payment
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Checkout([FromBody] CheckoutVM model)
        {
            if (!Function.IsLogin())
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để tiếp tục." });
                
            }

            if (ModelState.IsValid)
            {
                var cart = CART;
                var customerID = Function._AccountId;
                var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerID);
                ViewBag.Order =  _context.CarRentalOrders.Include(i => i.Status).Where(p => p.CustomerId == customerID).ToList();
                if (customer == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy khách hàng." });
                }

                if (string.IsNullOrWhiteSpace(customer.Address))
                {
                    return Json(new { success = false, message = "Vui lòng cập nhật địa chỉ nhận xe." });
                }


                
                decimal price = cart.Sum(p => p.PriceTotal);
                decimal deposit = (30*price)/100;
                decimal payment = price - deposit;
                var order = new CarRentalOrder
                {
                    CustomerId = customerID,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    Deposit = deposit,
                    //ReturnDate = DateOnly.FromDateTime(cart.Max(p => p.returnDate)),
                    StatusId = 1,
                    Payment = payment,
                    Notes = model.Notes,
                };

              


                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Add(order);
                        _context.SaveChanges();

                        var orderDetails = cart.Select(item => new OrderDetail
                        {
                            
                            OrderId = order.OrderId,
                            CarId = item.CartId,
                            Price = item.PriceTotal,
                            Quantity = item.Quantity,
                            PickupDate = item.pickupDate,
                            ReturnDate = item.returnDate,
                            
                        }).ToList();

                        _context.AddRange(orderDetails);
                        _context.SaveChanges();

                        HttpContext.Session.Set<List<CartItemsVM>>(CART_KEY, new List<CartItemsVM>());
                        transaction.Commit();

                        // Trả về URL của trang profile
                        return Json(new { success = true, redirectUrl = Url.Action("Index", "Accounts") });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = "Đã xảy ra lỗi. Vui lòng thử lại.", error = ex.Message });
                    }
                }
                
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }
        #region test checkout
        /*[HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                var cart = CART;
                var customerID = Function._AccountId;
                var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerID);
                //kiểm tra nếu khách hàng chưa cập nhật thôn tin địa chỉ thì phải quay về cập nhật
                if (customer.Address == null)
                {
                    TempData["ErrorMessage"] = "vui lòng cập nhật dịa chỉ nhận xe";
                    return RedirectToAction("Index", "Accounts");
                }

                var order = new CarRentalOrder
                {
                    CustomerId = customerID,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    Deposit = model.Depostit,
                    Payment = cart.Sum(p => p.Price),
                    ReturnDate = DateOnly.FromDateTime((DateTime)model.OrderReturn),
                    StatusId = 1

                };
                _context.Database.BeginTransaction();

                try
                {
                    _context.Database.CommitTransaction();
                    _context.Add(order);
                    _context.SaveChanges();
                    var oderDetails = new List<OrderDetail>();
                    foreach (var item in cart)
                    {
                        oderDetails.Add(new OrderDetail
                        {
                            OrderId = order.OrderId,
                            CarId = item.CartId,
                            Price = item.Price,
                            Quantity = item.Quantity,

                        });
                    }
                        _context.Add(oderDetails);
                        _context.SaveChanges();
                        HttpContext.Session.Set<List<CartItemsVM>>(CART_KEY, new List<CartItemsVM>());
                        return View("Success");
                } 
                catch
                {
                    _context.Database.RollbackTransaction();
                }

            }
            return View(CART);
        }*/
        #endregion end test

        public IActionResult Success()
        {
            return View();
        }
    }
    
}
