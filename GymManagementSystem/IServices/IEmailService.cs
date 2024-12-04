namespace GymManagementSystem.IServices
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body); 
    }
}
