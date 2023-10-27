using AutoMapper;
using MediPortal_Payments.Data;
using MediPortal_Payments.Models;
using MediPortal_Payments.Models.Dtos;
using MediPortal_Payments.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace MediPortal_Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;      
        private readonly IMapper _mapper;
        public PaymentService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
         
            _context = context;
        }
        public async Task<PaymentDto> CreatePayment(Guid UserId,PaymentRequestDto paymentRequest)
        {

            Payment newPayment = _mapper.Map<Payment>(paymentRequest);
            newPayment.UserId = UserId;
            newPayment.TotalCharges = Math.Round(paymentRequest.Prescription.Charges,2);
            newPayment.PaymentStatus = "pending";            
            PaymentDto Payment = new PaymentDto()
            {
                Name = paymentRequest.Name,
                Email = paymentRequest.Email,
                TotalCharges=paymentRequest.Prescription.Charges,
                PaymentStatus = "pending",
                Created = newPayment.Created,
                DoctorName=paymentRequest.Appointment.Doctor.firstname,
                Diagnosis=paymentRequest.Prescription.Diagnosis,
                Medication=paymentRequest.Prescription.Medication,
                
            };
           
            await _context.Payments.AddAsync(newPayment);
            await _context.SaveChangesAsync();

            return Payment;
        }

        public async Task<List<Payment>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto)
        {
            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };

            var sessionLineItems = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)(stripeRequestDto.payment.TotalCharges * 100),
                    Currency = "kes",
                    ProductData= new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name= "Medi Portal Payment",
                        Description= stripeRequestDto.payment.Medication,                        
                    },
                },
                Quantity = 1,
            };

            options.LineItems.Add(sessionLineItems);

            var service = new SessionService();
            Session session = service.Create(options);
            stripeRequestDto.StripeSessionId = session.Id;
            stripeRequestDto.StripeSessionUrl = session.Url;

            Payment payment = await _context.Payments.FirstOrDefaultAsync(x => x.PaymentId == stripeRequestDto.payment.PaymentId);

            payment.StripeSessionId = session.Id;
            await _context.SaveChangesAsync();

            return stripeRequestDto;
            

        }

        public async Task<bool> ValidatePayment(Guid PaymentId)
        {
            Payment payment = await _context.Payments.FirstOrDefaultAsync(x => x.PaymentId == PaymentId);
            var service = new SessionService();
            Session session = service.Get(payment.StripeSessionId);
            var paymentIntentService = new PaymentIntentService();
            var id = session.PaymentIntentId;
            if(id == null)
            {
                return false;
            };
            PaymentIntent paymentIntent = paymentIntentService.Get(id);
            if(paymentIntent.Status == "succeeded")
            {
                payment.PaymentIntentId = paymentIntent.Id;
                payment.PaymentStatus = "Approved";

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
