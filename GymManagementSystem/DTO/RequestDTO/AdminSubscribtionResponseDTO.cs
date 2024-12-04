namespace GymManagementSystem.DTO.RequestDTO
{
    public class AdminSubscribtionResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsSpecialOffer { get; set; }
        public bool IsNewSubscription { get; set; }
        public List<string> ProgramNames { get; set; }
        public List<SubPaymentResponseDTO>? SubscriptionPayments { get; set; }
    }
}
