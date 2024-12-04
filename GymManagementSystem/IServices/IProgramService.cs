using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;

namespace GymManagementSystem.IServices
{
    public interface IProgramService
    {
        Task<string> AddProgram(ProgramRequestDTO programRequestDTO);
        Task<string> AddSubscription(SubscriptionRequestDTO subscription);
        Task<string> AddSubscriptionPayment(Guid subscriptionId, SubscriptionPaymentRequestDTO subPayment);
        Task<string> AddSubscribedProgram(SubscribedProgramRequestDTO subProgram);
        Task<string> AddSubscribedProgramList(Guid subscriptionId, List<Guid> programIds);

        Task<string> DeleteProgram(Guid id);
        Task<string> DeleteSubscription(Guid id);
        Task<string> DeleteSubscriptionPayment(Guid id);
        Task<string> DeleteSubscribedProgram(Guid subscribeId, Guid programId);

        Task<string> UpdateProgram(Guid id, ProgramRequestDTO programRequestDTO);
        Task<string> UpdateProgramPayment(Guid id, decimal Amount);
        Task<SubscriptionResponseDTO> UpdateSubscription(Guid id, SubscriptionRequestDTO subscription);
        Task<string> UpdateSubscriptionPayment(Guid id, SubscriptionPaymentRequestDTO subPayment);

        Task<AdminSubscribtionResponseDTO> GetSingleSubscription(Guid subscriptionId);
        Task<List<AdminSubscribtionResponseDTO>> GetAllSubscriptions();
        Task<List<ProgramResponseDTO>> GetAllPrograms();
        Task<ProgramResponseDTO> GetSingleProgram(Guid Id);
        Task<string> AddProgramPayment(ProgramPaymentRequestDTO programPay);
    }
}
