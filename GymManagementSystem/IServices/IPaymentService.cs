using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;

namespace GymManagementSystem.IServices
{
    public interface IPaymentService
    {
        Task<string> AddPayment(PaymentRequestDTO payment);
        
        Task<List<PaymentResponseDTO>> GetAllPayments();
       
        Task<List<PaymentResponseDTO>> GetMemberPayments(Guid memberId);
      
        Task<List<MemberProgramResponseDTO>> GetOverDueMembers();
    }
}
