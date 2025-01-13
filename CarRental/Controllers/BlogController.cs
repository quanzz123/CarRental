
using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Controllers
{
    public class BlogController : Controller
    {
        private readonly DbRenalCarContext _context;
        public BlogController(DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
        [Route("/blog/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.Include(i => i.BlogComments).FirstOrDefaultAsync(i => i.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }
            ViewBag.blogComment = _context.BlogComments.Where(i => i.BlogId == id).ToList();
            return View(blog);
        }
    
    public async Task<IActionResult> AddBlogComment(string name, string email, string detail, int blogId)
        {
            try
            {
                // Tạo đối tượng BlogComment mới
                BlogComment c = new BlogComment
                {
                    Name = name,
                    Email = email,
                    Detail = detail,
                    CreatedDate = DateTime.Now,
                    BlogId = blogId,
                    IsActive = true // Mặc định là kích hoạt
                };

                // Thêm vào DbSet và lưu vào cơ sở dữ liệu
                _context.BlogComments.Add(c);
                await _context.SaveChangesAsync(); // Sử dụng async để đảm bảo dữ liệu được lưu

                // Trả về dữ liệu của comment vừa thêm
                return Json(new
                {
                    //status = true,
                    name = c.Name,
                    email = c.Email,
                    detail = c.Detail,
                   //createdDate = c.CreatedDate.ToString("dd/MM/yyyy")
                });
            }
            /*catch (Exception ex)
            {
                // Ghi log lỗi (nếu cần) và trả về trạng thái thất bại
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }*/
            catch (Exception ex)
            {
                // Ghi log lỗi (nếu cần) và trả về trạng thái thất bại
                return Json(new { status = false, message = ex.Message });
            }
        }

    }
}
