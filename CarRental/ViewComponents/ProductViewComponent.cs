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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var items = _context.Cars.Include(m => m.Type).OrderByDescending(m => m.CarId);
            var items = _context.Cars.OrderByDescending(m => m.CarId);

            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
