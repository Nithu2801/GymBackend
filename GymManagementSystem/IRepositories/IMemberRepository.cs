using GymManagementSystem.Entities;

namespace GymManagementSystem.IRepositories
{
    public interface IMemberRepository
    {
        User CreateUser(User user);
        //Task<List<Member>> GetAllMembers();
        List<Member> GetAllMembers();
        Member GetMember(Guid id);
        Member UpdateMember(Member member);
        List<Enrollment> GetMemberEnrollments(Guid Id);
        Enrollment CreateEnrollment(Enrollment enrollment);
        Enrollment GetEnrollments(Guid memberId, Guid programId);
        void DeleteEnrollment(Enrollment enrollment);
        Enrollment UpdateEnrollment(Enrollment enrollment);
        List<WorkoutProgram> NotEnrolledPrograms(List<Guid>? excludeProgramIds);
        Task<List<Enrollment>> GetOverDueEnrollments();
        Task<Member> GetMemberByUserId(string id);
    }
}
