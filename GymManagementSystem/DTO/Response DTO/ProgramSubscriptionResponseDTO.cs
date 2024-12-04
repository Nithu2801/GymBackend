using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.Response_DTO
{
    public class ProgramSubscriptionResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Duration { get; set; }
        public bool IsSpecialOffer { get; set; }
        public bool IsNewSubscription { get; set; }
    }
}
