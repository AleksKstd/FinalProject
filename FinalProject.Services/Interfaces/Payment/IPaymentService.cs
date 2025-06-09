using FinalProject.Services.DTOs.Payment;

namespace FinalProject.Services.Interfaces.Payment
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request);
        Task<GetAllUserPaymentsResponse> GetAllUserPayments(int userId);
    }
}
