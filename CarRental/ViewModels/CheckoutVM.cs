using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class CheckoutVM
    {
        [Required]
        public int CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal Deposit { get; set; }
        public decimal Payment {  get; set; }
        [Display(Name ="Ngày đặt")]
        [DataType(DataType.Date)]
        public DateTime? OrderReturn {  get; set; }
        public int StatusID { get; set; }
    }
}
