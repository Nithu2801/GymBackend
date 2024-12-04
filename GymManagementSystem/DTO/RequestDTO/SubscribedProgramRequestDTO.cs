namespace GymManagementSystem.DTO.RequestDTO
{
    public class SubscribedProgramRequestDTO
    {
        public Guid SubscriptionId { get; set; }
        public Guid ProgramId { get; set; }
        public List<ProgramPaymentRequestDTO> PaymentRequests { get; set; }
    }
}
