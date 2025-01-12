using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.ViewComponents
{
    public class CategoriesVIewComponent : ViewComponent
    {
        private readonly DbRenalCarContext _context;
        public CategoriesVIewComponent(DbRenalCarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _context.CarTypes.ToList();
            return View(categories);
        }
    }
}
