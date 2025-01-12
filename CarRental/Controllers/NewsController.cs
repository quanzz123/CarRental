using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    public class NewsController : Controller
    {
        private readonly DbRenalCarContext _context;
        public NewsController(DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/news/{alias}-{id}.html")]
        public async Task<IActionResult> Details(string alias, int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }
            var news = await _context.News.FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }
            ViewBag.newsComment = _context.NewsComments.Where(r => r.NewsId == news.NewsId).ToList();
            return View(news);

        }
        public async Task<IActionResult> Comment(string name, string email, string detail, int id)
        {
            try
            {
                // Tạo đối tượng review mới
                NewsComment r = new NewsComment
                {
                    Name = name,
                    Email = email,
                    CreatedDate = DateTime.Now,
                    Detail = detail,
                    NewsId = id,
                  

                };

                // Thêm vào DbSet và lưu vào cơ sở dữ liệu
                _context.NewsComments.Add(r);
                await _context.SaveChangesAsync(); // Sử dụng await để đảm bảo dữ liệu được lưu


                // Trả về dữ liệu của review vừa được thêm
                return Json(new
                {
                    //status = true,
                    name = r.Name,
                    email = r.Email,
                    detail = r.Detail,
                

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
