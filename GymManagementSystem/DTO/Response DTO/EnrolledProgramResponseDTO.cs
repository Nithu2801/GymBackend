using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.Response_DTO
{
    public class EnrolledProgramResponseDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<ProgramImageResponseDTO>? Images { get; set; }
        public ProgramSubscriptionResponseDTO ProgramSubscription { get; set; }
        public List<ProgramPaymentResponseDTO>? ProgramPayments { get; set; }
    }
}
