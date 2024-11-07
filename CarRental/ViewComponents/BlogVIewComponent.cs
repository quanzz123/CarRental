using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace CarRental.ViewComponents
{
    public class BlogVIewComponent : ViewComponent

    {
        private readonly DbRenalCarContext _context;
        public BlogVIewComponent (DbRenalCarContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = _context.Blogs.Where(i => (bool)i.IsActive).ToList();
            return await Task.FromResult<IViewComponentResult>(View(item));
        }
    }
}
