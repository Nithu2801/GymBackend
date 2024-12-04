using GymManagementSystem.DBContext;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class MemberRepository: IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            var data = _context.Users.Add(user);
            _context.SaveChanges();
            return data.Entity;
        }

        public List<Member> GetAllMembers()
        {
            var data =  _context.Members
                .Include(m => m.User) 
                .Where(i => i.MemberStatus == true) 
                .ToList();

            return data;
        }
        public async Task<Member> GetMemberByUserId(string id)
        {
            var data =await _context.Members
                .Include(m => m.User)
                .Where(i => i.MemberStatus == true)
                .SingleOrDefaultAsync(i => i.UserId == id);
            if (data == null)
            {
                throw new Exception("Member is not found");
            }
            return data;
        }
        public Member GetMember(Guid id)
        {
            var data =  _context.Members
                .Include(m => m.User)
                .Where(i => i.MemberStatus == true)
                .FirstOrDefault(i => i.Id == id);
            if (data == null)
            {
                throw new Exception("Member is not found");
            }
            return data;
        }
        //Member update and delete (soft delete)
        public Member UpdateMember(Member member)
        {
            var data = _context.Members.Update(member);
             _context.SaveChangesAsync();
            return data.Entity;
        }
        public void DeleteMember(Member member)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
        }

        //Enrollment
        public  Enrollment CreateEnrollment(Enrollment enrollment)
        {
            var data = _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
            return data.Entity;
        }
        public Enrollment GetEnrollments(Guid memberId,Guid programId)
        {
            var data =  _context.Enrollments.Where(i => i.MemberId == memberId).FirstOrDefault(j=>j.ProgramId==programId);
            return data;
        }
        public List<Enrollment> GetMemberEnrollments(Guid Id)
        {
            var data =  _context.Enrollments.Include(i => i.Program).Include(p => p.Member).Where(i => i.MemberId == Id).ToList();
            return data;
        }
        public async Task<List<Enrollment>> GetOverDueEnrollments()
        {
            var data = await _context.Enrollments.Include(i => i.Member).Include(j => j.Program).Include(k=>k.Subscription).Where(x => x.NextDueDate < DateTime.Now).ToListAsync();
            return data;
        }
        public List<Enrollment> GetAllProgramEnrollments(Guid Id)
        {
            var data =  _context.Enrollments.Include(i => i.Program).Include(p => p.Member).Where(i => i.ProgramId == Id).ToList();
            return data;
        }
        public List<Enrollment> GetAllEnrollment()
        {
            var data =  _context.Enrollments.Include(i => i.Program).Include(p => p.Member).ToList();
            return data;
        }
        public Enrollment UpdateEnrollment(Enrollment enrollment)
        {
            var data = _context.Enrollments.Update(enrollment);
            return data.Entity;
        }
        public List<WorkoutProgram> NotEnrolledPrograms(List<Guid>? excludeProgramIds)
        {
            if (excludeProgramIds == null || !excludeProgramIds.Any())
            {
                return  _context.WorkoutPrograms.ToList();
            }

            var data =  _context.WorkoutPrograms
                .Where(p => !excludeProgramIds.Contains(p.Id))
                .ToList();

            return data;
        }
        public void DeleteEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
        }
    }
}
