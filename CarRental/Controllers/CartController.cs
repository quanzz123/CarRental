using CarRental.Extensions;
using CarRental.Models;
using CarRental.Utilities;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public IActionResult AddToCart(int id, int quantity = 1, string type = "Normal")
        {

            var cart = CART;
            // lấy thông tin sản phảm từ DB
            var product = _context.Cars.FirstOrDefault(p => p.CarId == id);
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
                    Price = finalPrice,
                    Quantity = quantity,
                    
                };
                cart.Add(CarItem);
            } else
            {
                // nếu đãn có thì tăng số lượng len
                CarItem.Quantity += quantity;
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

                return RedirectToAction("Index", "Login");
            }
            if (CART == null)
            {
                return View("/");
            }
            return View(CART);
        }

        [HttpPost]
        public IActionResult Checkout(Customer c)
        {
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            if (CART == null)
            {
                return View("/");
            }
            return View(CART);
        }
    }
}
