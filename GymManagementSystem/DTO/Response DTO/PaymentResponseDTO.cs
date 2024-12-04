namespace GymManagementSystem.DTO.Response_DTO
{
    public class PaymentResponseDTO
    {
        public Guid MemberId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }
        public string ProgramName { get; set; }
        public DateTime Date {  get; set; }
    }
}
