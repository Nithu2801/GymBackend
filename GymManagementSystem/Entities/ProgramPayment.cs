using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class ProgramPayment
    {
        public Guid Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public bool Status { get; set; }=true;
        public Guid ProgramId { get; set; }
        public WorkoutProgram Program { get; set; }
        public Guid SubscriptionPaymentId { get; set; }
        public SubscriptionPayment SubscriptionPayment { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
