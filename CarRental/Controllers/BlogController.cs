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
            //kiem tra trang thai dang nhap
            if (!Function.IsLogin())
            {

                return RedirectToAction("Index", "Login");
            }
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
            return View(blog);
        }
    }
}
