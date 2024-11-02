using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.ViewComponents
{
    public class CarTypesViewComponent :ViewComponent
    {
        private readonly DbRenalCarContext _context;

        public CarTypesViewComponent(DbRenalCarContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cartype = _context.CarTypes;
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
