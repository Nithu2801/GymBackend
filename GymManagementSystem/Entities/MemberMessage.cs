using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class MemberMessage
    {
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
    }
}
