using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string ReviewMessage { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }

    }
}
