using CarRental.Extensions;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CartController : Controller
    {
        private readonly DbRenalCarContext _context;
        public CartController(DbRenalCarContext context)
        {
            _context = context;
        }
        const string CART_KEY = "MYCART";
        public List<Car> CART => HttpContext.Session.Get<List<Car>>(CART_KEY) ?? new List<Car> ();
        public IActionResult Index()
        {
            return View(CART);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = CART;
            var item = cart.FirstOrDefault(p => p.CarId == id);
            if (item == null) { 
                
            }
            return View();
        }
    }
}
