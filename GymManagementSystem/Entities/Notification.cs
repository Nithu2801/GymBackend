using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Message { get; set; } 
        public Priority Priority {  get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
    }
    public enum Priority
    {
        Low,
        Medium,
        Heigh
    }
}
