using GymManagementSystem.Entities;

namespace GymManagementSystem.IRepositories
{
    public interface IMessageRepository
    {
        AdminMessage AddAdminMessage(AdminMessage message);
        MemberMessage AddMemberMessage(MemberMessage memberMessage);
        VisitorMessage AddVisitorMessage(VisitorMessage visitorMessage);
        Review CreateReview(Review review);
        void DeleteAdminMessage(AdminMessage member);
        void DeleteMemberMessage(MemberMessage memberMessage);
        void DeleteReview(Review review);
        void DeleteVisitorMessage(VisitorMessage visitorMessage);
        Task<List<AdminMessage>> GetAdminMessage(Guid id);
        Task<List<AdminMessage>> GetAllAdminMessages();
        Task<List<MemberMessage>> GetAllMessages();
        Task<List<Review>> GetAllReviews();
        Task<List<VisitorMessage>> GetAllVisitorMessage();
        Task<List<MemberMessage>> GetMemberMessage(Guid id);
        Task<Review> GetReview(Guid id);
        Task<MemberMessage> GetSingleMessage(Guid id);
        Task<AdminMessage> GetSinglrAdminMessage(Guid id);
        Task<VisitorMessage> GetVisitorMessage(Guid id);
        Task<AdminMessage> UpdateAdminMessage(AdminMessage member);
        Task<MemberMessage> UpdateMemberMessage(MemberMessage memberMessage);
        Task<Review> UpdateReview(Review review);
        Task<VisitorMessage> UpdateVisitorMessage(VisitorMessage visitorMessage);
    }
}
