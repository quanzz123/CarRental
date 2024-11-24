using CarRental.Extensions;
using CarRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            //var product = await _context.Cars.Include(i=>i.Type).FirstOrDefaultAsync(m => m.CarId == id);
            var product = await _context.Cars.FirstOrDefaultAsync(m => m.CarId == id);

            if (product == null)
            {
                return NotFound();
            }

            //ViewBag.productFeatured = _context.CarTypes.OrderByDescending(i=>i.CarTypeId).ToList();


            //ViewBag.productRelated = _context.Cars.Include(i=>i.Type).Where(i => i.TypeId == product.TypeId && i.CarId != id).ToList();
            //ViewBag.productRelated = _context.Cars.Where(i => i.TypeId == product.TypeId && i.CarId != id).ToList();

            // Lấy danh sách sản phẩm đã xem gần đây từ session
            var recentProducts = HttpContext.Session.Get<List<int>>("RecentProducts") ?? new List<int>();

            // Thêm ID sản phẩm vào danh sách nếu chưa có
            if (!recentProducts.Contains(id.Value))
            {
                recentProducts.Add(id.Value);

                // Giữ danh sách giới hạn số lượng sản phẩm (ví dụ: 5 sản phẩm gần đây)
                if (recentProducts.Count > 5)
                {
                    recentProducts.RemoveAt(0);
                }

                // Cập nhật session
                HttpContext.Session.Set("RecentProducts", recentProducts);
            }
            return View(product);
        }
                 

    }
}
