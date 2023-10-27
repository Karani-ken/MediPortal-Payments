namespace MediPortal_Payments.Models.Dtos
{
    public class PrescriptionDto
    {      


        public string Diagnosis { get; set; } = string.Empty;

        public string Medication { get; set; } = string.Empty;

        public double Charges { get; set; }
    }
}
