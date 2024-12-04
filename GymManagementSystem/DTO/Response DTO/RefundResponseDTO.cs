namespace GymManagementSystem.DTO.Response_DTO
{
    public class RefundResponseDTO
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public Guid MemberId { get; set; }
    }
}
