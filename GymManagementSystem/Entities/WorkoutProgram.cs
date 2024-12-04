using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class WorkoutProgram
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool Status { get; set; } = true;
        public ICollection<ProgramImages>? Images { get; set; }
        public ICollection<ProgramPayment>? ProgramPayments { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<SubscribedProgram>? Subscriptions { get; set; }
    }
}
