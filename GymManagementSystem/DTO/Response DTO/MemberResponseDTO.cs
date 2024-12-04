namespace GymManagementSystem.DTO.Response_DTO
{
    public class MemberResponseDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string? ProfileImage { get; set; }
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
    }
}
