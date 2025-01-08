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

        public async Task<IActionResult> Details(int? id)
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
            
            return View(news);

        }
    }
}
