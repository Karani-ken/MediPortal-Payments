namespace MediPortal_Payments.Models.Dtos
{
    public class AppointmentDto
    {
         public Guid AppointmentId {get; set;}
        public string AppointmentType { get; set; } = string.Empty;

        public DoctorDto? Doctor { get; set; }

        public HospitalDto? Hospital { get; set; }

        // public PatientDto? PatientDto { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Slot { get; set; } = string.Empty;

        public string Symptoms { get; set; } = string.Empty;

        public string AppointmentStatus { get; set; } = string.Empty;
    }
}
