namespace MediPortal_Payments.Models.Dtos
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public double TotalCharges { get; set; }
        public string? PaymentStatus { get; set; } = string.Empty;

        public DateTime? Created { get; set; } = DateTime.Now;      
        public string? DoctorName { get; set; } 
         public Guid AppointmentId {get; set;}
        public string? Diagnosis { get; set; }
        public string? Medication { get; set; }
      
    }
}
