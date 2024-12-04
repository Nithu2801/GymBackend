using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;

namespace GymManagementSystem.IServices
{
    public interface IMessageService
    {
        Task<string> AddAdminMessage(MessageRequestDTO message);
        Task<string> AddMemberMessage(MessageRequestDTO message);
        Task<string> DeleteAdminMessage(Guid id);
        Task<string> DeleteMemberMessage(Guid id);
        Task<List<MessageResponseDTO>> GetAdminMessageToSingleMember(Guid MemberId);
        Task<List<MessageResponseDTO>> GetAllAdminMessage();
        Task<List<MessageResponseDTO>> GetAllMemberMessage();
        Task<List<MessageResponseDTO>> GetSingleMemberMessage(Guid MemberId);
    }
}
