namespace CarRental.ViewModels
{
    public class CartItemsVM
    {
        public int CartId { get; set; }
        public string CarName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PriceTotal => Price * Quantity;
    }
}
