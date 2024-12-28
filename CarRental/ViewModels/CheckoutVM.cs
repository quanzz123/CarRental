using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class CheckoutVM
    {
        [Required]
        public int CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal Depostit { get; set; }
        public decimal Payment {  get; set; }
        public DateTime? OrderReturn {  get; set; }
        public int StatusID { get; set; }
    }
}
