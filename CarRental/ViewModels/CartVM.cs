namespace CarRental.ViewModels
{
    public class CartVM
    {
        public decimal Total { get; set; }
        public decimal Deposit { get; set; }
        public decimal Payment { get; set; }
        public DateTime pickupDate { get; set; }
        public DateTime returnDate { get; set; }
    }
}
