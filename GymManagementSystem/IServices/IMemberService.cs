using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;
using GymManagementSystem.Entities;

namespace GymManagementSystem.IServices
{
    public interface IMemberService
    {
        Task<string> CreateMember(MemberRequestDTO memberRequestDTO);
        Task<string> CreateAdmin(AdminRequestDTO adminRequestDTO);
        Task<string> AddEnrollment(EnrollmentRequestDTO enrollmentRequest);
        Task<List<MemberResponseDTO>> GetAllMemberDetails();
        Task<MemberResponseDTO> GetSingleMember(Guid id);
        Task<List<ProgramResponseDTO>> GetEnrollablePrograms(Guid memberId);
        Task<List<EnrolledProgramResponseDTO>> GetMemberEnrolledPrograms(Guid id);
        Task<string> UpdateMember(Guid Id, MemberRequestDTO memberRequestDTO);
        Task<string> DeleteMember(Guid Id);
        Task<string> DeleteEnrollment(Guid memberId, Guid programId);
        Task<MemberResponseDTO> GetMemberByUserId(string id);
    }
}
