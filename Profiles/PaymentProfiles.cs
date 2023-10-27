using AutoMapper;
using MediPortal_Payments.Models;
using MediPortal_Payments.Models.Dtos;

namespace MediPortal_Payments.Profiles
{
    public class PaymentProfiles:Profile
    {
        public PaymentProfiles()
        {
            CreateMap<Payment, PaymentRequestDto>().ReverseMap();
            CreateMap<PaymentDto, Payment>().ReverseMap();
        }
    }
}
