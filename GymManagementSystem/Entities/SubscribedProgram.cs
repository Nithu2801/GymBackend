using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class SubscribedProgram
    {
        [Key]
        public Guid SubscribeId { get; set; }
        public Subscription Subscription { get; set; }
        [Key]
        public Guid ProgramId { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
