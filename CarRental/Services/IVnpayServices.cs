using CarRental.ViewModels;

namespace CarRental.Services
{
    public interface IVnpayServices
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
