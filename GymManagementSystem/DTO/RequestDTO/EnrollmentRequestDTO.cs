using GymManagementSystem.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.RequestDTO
{
    public class EnrollmentRequestDTO
    {

        public Guid MemberId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid SubscriptionId { get; set; }

    }
}
