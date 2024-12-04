namespace GymManagementSystem.DTO.RequestDTO
{
    public class SubscriptionRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsSpecialOffer { get; set; }
        public List<SubscriptionPaymentRequestDTO> PaymentRequests { get; set; }
    }
}
