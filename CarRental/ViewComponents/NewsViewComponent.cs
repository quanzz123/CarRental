using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.ViewComponents
{
    public class NewsViewComponent : ViewComponent
    {
        private readonly DbRenalCarContext _context;

        public NewsViewComponent(DbRenalCarContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.News.Where(m => (bool)m.IsActive).
                OrderBy(m => m.NewsId).ToList();
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
