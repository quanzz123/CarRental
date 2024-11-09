using CarRental.Models;
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.Include(i => i.BlogId).FirstOrDefaultAsync(m => m.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}
