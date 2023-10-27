using MediPortal_Payments.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MediPortal_Payments.Models
{
    public class Payment
    {
        [Key]
        public Guid  PaymentId { get; set; }
        public Guid? UserId { get; set; }
        public Guid AppointmentId {get; set;}
        public string? Name { get; set; }
        public string? Email { get; set; }
        public double TotalCharges { get; set; }
        public string? PaymentStatus { get; set; } = string.Empty;

        public DateTime? Created { get; set; } = DateTime.Now;

        public string? PaymentIntentId { get; set; }

        public string? StripeSessionId { get; set; }
               

    }
}
