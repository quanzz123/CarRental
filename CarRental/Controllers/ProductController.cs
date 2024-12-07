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
            var product = await _context.Cars.Include(i=>i.Type).FirstOrDefaultAsync(m => m.CarId == id);
            //var product = await _context.Cars.FirstOrDefaultAsync(m => m.CarId == id);

            if (product == null)
            {
                return NotFound();
            }

            //ViewBag.productFeatured = _context.CarTypes.OrderByDescending(i=>i.CarTypeId).ToList();


            //ViewBag.productRelated = _context.Cars.Include(i=>i.Type).Where(i => i.TypeId == product.TypeId && i.CarId != id).ToList();
            ViewBag.productRelated = _context.Cars.Where(i => i.TypeId == product.TypeId && i.CarId != id).ToList();
            ViewBag.productRviews = _context.CarReviews.Where(r => r.CarId == product.CarId).ToList();
          
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
        [HttpPost]
        public async Task<IActionResult> Reviews(string name,string email, string detail, int id, int star)
        {
            try
            {
                // Tạo đối tượng review mới
                CarReview r = new CarReview
                {
                    Name = name,
                    
                    Email = email,
                    CreatedDate = DateTime.Now,
                    Detail= detail,
                    CarId = id,
                    Star = star

                };

                // Thêm vào DbSet và lưu vào cơ sở dữ liệu
                _context.CarReviews.Add(r);
                await _context.SaveChangesAsync(); // Sử dụng await để đảm bảo dữ liệu được lưu


                // Trả về dữ liệu của review vừa được thêm
                return Json(new
                {
                    status = true,
                    review = new
                    {
                        r.Name,
                        r.Detail,
                        r.Star,
                        CreatedDate = r.CreatedDate.Value.ToString("dd/MM/yyyy")
                    }
                });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (nếu cần) và trả về trạng thái thất bại
                return Json(new { status = false, message = ex.Message });
            }
        }
                 

    }
}
