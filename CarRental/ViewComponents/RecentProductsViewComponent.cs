using CarRental.Extensions;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.ViewComponents
{
    public class RecentProductsViewComponent : ViewComponent
    {
        private readonly DbRenalCarContext _context;

        public RecentProductsViewComponent(DbRenalCarContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách ID sản phẩm gần đây từ session
            var recentProductIds = HttpContext.Session.Get<List<int>>("RecentProducts") ?? new List<int>();

            // Lấy chi tiết các sản phẩm từ cơ sở dữ liệu
            var recentProducts = _context.Cars
                .Where(p => recentProductIds.Contains(p.CarId))
              .ToList();

            return View(recentProducts);
        }
    }
}
