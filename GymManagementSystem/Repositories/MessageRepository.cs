using GymManagementSystem.DBContext;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class MessageRepository: IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }
        public AdminMessage AddAdminMessage(AdminMessage message)
        {
            try
            {

            var data = _context.AdminMessages.Add(message);
            _context.SaveChanges();
            return data.Entity;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<List<AdminMessage>> GetAllAdminMessages()
        {
            var data = await _context.AdminMessages.Include(m => m.Member).Where(i => i.Status == true).ToListAsync();
            return data;
        }
        public async Task<List<AdminMessage>> GetAdminMessage(Guid id)
        {
            var data = await _context.AdminMessages.Include(m => m.Member).Where(i => i.Status == true).Where(i => i.MemberId == id).ToListAsync();
            return data;
        }
        public async Task<AdminMessage> GetSinglrAdminMessage(Guid id)
        {
            var data = await _context.AdminMessages.Include(m => m.Member).Where(i => i.Status == true).FirstOrDefaultAsync(i => i.MemberId == id);
            if (data == null)
            {
                throw new Exception("AdminMessage is not found");
            }
            return data;
        }
        public async Task<AdminMessage> UpdateAdminMessage(AdminMessage member)
        {
            var data = _context.AdminMessages.Update(member);
            await _context.SaveChangesAsync();
            return data.Entity;
        }
        public void DeleteAdminMessage(AdminMessage member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            _context.AdminMessages.Remove(member);
            _context.SaveChanges();
        }

        //MemberMessage
        public MemberMessage AddMemberMessage(MemberMessage memberMessage)
        {
            var data = _context.MemberMessages.Add(memberMessage);
            _context.SaveChanges();
            return data.Entity;
        }
        public async Task<List<MemberMessage>> GetAllMessages()
        {
            var data = await _context.MemberMessages.Include(m => m.Member).Where(i => i.Status == true).ToListAsync();
            return data;
        }
        public async Task<List<MemberMessage>> GetMemberMessage(Guid id)
        {
            var data = await _context.MemberMessages.Include(m => m.Member).Where(i => i.Status == true).Where(i => i.MemberId == id).ToListAsync();
            return data;
        }
        public async Task<MemberMessage> GetSingleMessage(Guid id)
        {
            var data = await _context.MemberMessages.Include(m => m.Member).Where(i => i.Status == true).FirstOrDefaultAsync(i => i.Id == id);
            if (data == null)
            {
                throw new Exception("MemberMessage is not found");
            }
            return data;
        }
        public async Task<MemberMessage> UpdateMemberMessage(MemberMessage memberMessage)
        {
            var data = _context.MemberMessages.Update(memberMessage);
            await _context.SaveChangesAsync();
            return data.Entity;
        }
        public void DeleteMemberMessage(MemberMessage memberMessage)
        {
            _context.MemberMessages.Remove(memberMessage);
            _context.SaveChanges();
        }

        //Visitor Messge
        public VisitorMessage AddVisitorMessage(VisitorMessage visitorMessage)
        {
            var data = _context.VisitorMessages.Add(visitorMessage);
            _context.SaveChanges();
            return data.Entity;
        }
        public async Task<List<VisitorMessage>> GetAllVisitorMessage()
        {
            var data = await _context.VisitorMessages.Where(i => i.Status == true).ToListAsync();
            return data;
        }
        public async Task<VisitorMessage> GetVisitorMessage(Guid id)
        {
            var data = await _context.VisitorMessages.Where(i => i.Status == true).FirstOrDefaultAsync(i => i.Id == id);
            if (data == null)
            {
                throw new Exception("VisitorMessage is not found");
            }
            return data;
        }
        public async Task<VisitorMessage> UpdateVisitorMessage(VisitorMessage visitorMessage)
        {
            var data = _context.VisitorMessages.Update(visitorMessage);
            await _context.SaveChangesAsync();
            return data.Entity;
        }
        public void DeleteVisitorMessage(VisitorMessage visitorMessage)
        {
            _context.VisitorMessages.Remove(visitorMessage);
            _context.SaveChanges();
        }

        //Review
        public Review CreateReview(Review review)
        {
            var data = _context.Reviews.Add(review);
            _context.SaveChanges();
            return data.Entity;
        }
        public async Task<List<Review>> GetAllReviews()
        {
            var data = await _context.Reviews.Include(m => m.Member).ToListAsync();
            return data;
        }
        public async Task<Review> GetReview(Guid id)
        {
            var data = await _context.Reviews.Include(m => m.Member).FirstOrDefaultAsync(i => i.Id == id);
            return data;
        }
        //Member update and delete (soft delete)
        public async Task<Review> UpdateReview(Review review)
        {
            var data = _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return data.Entity;
        }
        public void DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}
