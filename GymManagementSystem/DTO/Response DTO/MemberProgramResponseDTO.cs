namespace GymManagementSystem.DTO.Response_DTO
{
    public class MemberProgramResponseDTO
    {
        public Guid MemberId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Address { get; set; }
        public string NicNo { get; set; }
        public Guid ProgramId { get; set; }
        public string Name { get; set; }
        public string SubscriptionTitle { get; set; }
    }
}
