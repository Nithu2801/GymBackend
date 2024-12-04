using GymManagementSystem.DBContext;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public Payment AddPayment(Payment payment)
        {
            var data = _context.Payments.Add(payment);
            _context.SaveChanges();
            return data.Entity;
        }
        public async Task<List<Payment>> GetAllPayments()
        {
            var data = await _context.Payments.Include(m => m.Member).ToListAsync();
            return data;
        }
        public async Task<List<Payment>> GetMemberPayments(Guid id)
        {
            var data = await _context.Payments.Include(m => m.Member).Where(i => i.MemberId == id).ToListAsync();
            return data;
        }
        public Payment GetSinglePayment(Guid id)
        {
            var data =  _context.Payments.Include(m => m.Member).FirstOrDefault(i => i.Id == id);
            if (data == null)
            {
                throw new Exception("Payment is not found");
            }
            return data;
        }
        public Payment UpdatePayment(Payment member)    
        {
            var data = _context.Payments.Update(member);
            _context.SaveChanges();
            return data.Entity;
        }
        public void DeletePayment(Payment member)
        {
            _context.Payments.Remove(member);
            _context.SaveChanges();
        }


       
    }
}
