using GymManagementSystem.Entities;

namespace GymManagementSystem.IRepositories
{
    public interface IPaymentRepository
    {
        Payment AddPayment(Payment payment);
      
        void DeletePayment(Payment member);
       
        Task<List<Payment>> GetAllPayments();
       
        Task<List<Payment>> GetMemberPayments(Guid id);
        
        Payment GetSinglePayment(Guid id);
        Payment UpdatePayment(Payment member);
     
    }
}
