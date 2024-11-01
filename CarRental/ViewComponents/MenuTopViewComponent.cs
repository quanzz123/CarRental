using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.ViewComponents
{
    public class MenuTopViewComponent : ViewComponent
    {
        private readonly DbRenalCarContext _context;

        public MenuTopViewComponent(DbRenalCarContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.Menus.Where(m => (bool)m.IsActive)
                .OrderBy(m => m.Position).ToList();
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
