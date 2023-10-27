using MediPortal_Payments.Models;
using MediPortal_Payments.Models.Dtos;

namespace MediPortal_Payments.Services.IServices
{
    public interface IPaymentService
    {
        Task<PaymentDto> CreatePayment(Guid userId,PaymentRequestDto paymentRequest);

        Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto);
        Task<List<Payment>> GetPayments();
        Task<bool> ValidatePayment(Guid PaymentId);
    }
}
