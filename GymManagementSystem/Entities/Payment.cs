using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Guid ProgramPaymentId {  get; set; }
        public ProgramPayment ProgramPayment { get; set; }
    }
}
