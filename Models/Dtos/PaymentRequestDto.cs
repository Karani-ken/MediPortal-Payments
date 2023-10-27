namespace MediPortal_Payments.Models.Dtos
{
    public class PaymentRequestDto
    {        

        public string? Name { get; set; }
        public string? Email { get; set; }
        public double? TotalCharges { get; set; }
        public string? PaymentStatus { get; set; } = string.Empty;      

        public string? PaymentIntentId { get; set; }

        public string? StripeSessionId { get; set; }
        public Guid AppointmentId {get; set;}
        public AppointmentDto? Appointment { get; set; }  
       
        public PrescriptionDto? Prescription { get; set; }
     
        
    }
}
