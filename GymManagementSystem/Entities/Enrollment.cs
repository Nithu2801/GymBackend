using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementSystem.Entities
{
    public class Enrollment
    {
        [Key, Column(Order = 0)]
        public Guid MemberId { get; set; }
        public virtual Member Member { get; set; }
        [Key, Column(Order = 1)]
        public Guid ProgramId { get; set; }
        public virtual WorkoutProgram Program { get; set; }
        [Required]
        public DateTime EnrolledDate { get; set; }
        [Required]
        public DateTime NextDueDate { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
