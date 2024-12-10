using CarRental.Extensions;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.ViewComponents
{
    public class MiniCartVIewComponent:ViewComponent
    {
        const string CART_KEY = "MYCART";
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = HttpContext.Session.Get<List<CartItemsVM>>(CART_KEY) ?? new List<CartItemsVM>();
            return View(cart);
        }
    }
}
