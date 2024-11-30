using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly DbRenalCarContext _context;
        public BlogsController (DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var blog = _context.Blogs.OrderBy(b=>b.BlogId).ToList();
            return View(blog);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog b)
        {
            if (ModelState.IsValid)
            {
                b.Alias = CarRental.Utilities.Function.TitleSlugGenerationAlias(b.Title);
                _context.Blogs.Add(b);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var blog = _context.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog b)
        {
            if (ModelState.IsValid) {
                b.Alias = CarRental.Utilities.Function.TitleSlugGenerationAlias(b.Title);
                _context.Blogs.Update(b);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }

        public IActionResult Delete(int? id) { 
            if (id == 0 || id == null)
            {
                return NotFound(); 
            }
            var blog = _context.Blogs.Find(id);
            if (blog == null) {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleBlog = _context.Blogs.Find(id);
            if (deleBlog == null)
            {
                return NotFound();
            }
            _context.Blogs.Remove(deleBlog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
