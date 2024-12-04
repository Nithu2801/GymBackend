namespace GymManagementSystem.Entities
{
    public class VisitorMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
