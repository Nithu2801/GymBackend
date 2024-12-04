namespace GymManagementSystem.DTO.Response_DTO
{
    public class SkippedPaymentResponseDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public string Reason { get; set; }
        public Guid MemberId { get; set; }
        public Guid ProgramId { get; set; }
    }
}
