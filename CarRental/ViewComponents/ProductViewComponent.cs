using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.ViewComponents
{
    public class ProductViewComponent :ViewComponent
    {
        private readonly DbRenalCarContext _context;

        public ProductViewComponent(DbRenalCarContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int? typeId)
        {
            var categories = _context.Cars.AsQueryable();

            // Nếu cần lọc theo typeId (nếu có)
            if (typeId.HasValue)
            {
                categories = categories.Where(c => c.TypeId == typeId.Value);
            }

            return View(categories.ToList());
        }
    }
}
