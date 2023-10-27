namespace MediPortal_Payments.Models.Dtos
{
    public class ResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public bool IsSuccess { get; set; } = true;

        public Object? obj { get; set; }
    }
}
