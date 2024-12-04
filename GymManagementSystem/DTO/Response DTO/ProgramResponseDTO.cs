using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.Response_DTO
{
    public class ProgramResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public bool? IsProgramNew { get; set; }
        public List<ProgramImageResponseDTO>? Images { get; set; }
        public List<SubscriptionResponseDTO>? Subscriptions { get; set; }
    }
}
