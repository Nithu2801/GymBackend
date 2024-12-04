namespace GymManagementSystem.DTO.RequestDTO
{
    public class ProgramPaymentRequestDTO
    {
        public Guid ProgramId { get; set; }
        public Guid SubPaymentId { get; set; }
        public decimal Amount { get; set; }
    }
}
