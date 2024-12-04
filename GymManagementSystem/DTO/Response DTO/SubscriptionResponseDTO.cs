using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.Response_DTO
{
    public class SubscriptionResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsSpecialOffer { get; set; }
        public bool IsNewSubscription { get; set; }
        public List<ProgramPaymentResponseDTO> Payments { get; set; }
    }
}
