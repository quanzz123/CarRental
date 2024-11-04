using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbRenalCarContext _context;
        public ProductController(DbRenalCarContext context)
        {
            _context = context;
        }
        /*[Route("/Product/Index.html")]*/
        public IActionResult Index()
        {
            return View();
        }
        [Route("/product/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }
            var product = await _context.Cars.Include(i=>i.CarType).FirstOrDefaultAsync(m => m.CarId == id);

            if (product == null)
            {
                return NotFound();
            }
            
            ViewBag.productFeatured = _context.Cars.OrderByDescending(i => i.Rate).ToList();
            ViewBag.productRelated = _context.Cars.Include(i=>i.CarType).Where(i => i.CarTypeId == product.CarTypeId && i.CarId != id).ToList();
            return View(product);
        }
                 

    }
}
