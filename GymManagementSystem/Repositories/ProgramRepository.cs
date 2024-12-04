using GymManagementSystem.DBContext;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly AppDbContext _context;

        public ProgramRepository(AppDbContext context)
        {
            _context = context;
        }
        //Program
        public WorkoutProgram CreateProgram(WorkoutProgram workoutProgram)
        {
            var data = _context.WorkoutPrograms.Add(workoutProgram);
            _context.SaveChanges();
            return data.Entity;
        }
        public WorkoutProgram GetWorkoutProgram(Guid id)
        {
            var data =  _context.WorkoutPrograms.Include(i => i.ProgramPayments.Where(p => p.Status == true)).Include(p => p.Images).Where(i => i.Status == true).FirstOrDefault(i => i.Id == id);
            if (data == null)
            {
                throw new Exception("Program not found");
            }
            return data;
        }
        public List<WorkoutProgram> GetAllWorkoutProgram()
        {
            var data =  _context.WorkoutPrograms.Include(i => i.ProgramPayments.Where(p=>p.Status==true)).Where(i => i.Status == true).Include(p => p.Images).ToList();
            return data;
        }
        public WorkoutProgram UpdateProgram(WorkoutProgram workoutProgram)
        {
            var data = _context.WorkoutPrograms.Update(workoutProgram);
             _context.SaveChangesAsync();
            return data.Entity;
        }
        public void DeleteProgram(WorkoutProgram workoutProgram)
        {
            _context.WorkoutPrograms.Remove(workoutProgram);
            _context.SaveChanges();
        }

        //programpayment
        public ProgramPayment UpdateProgramPayment(ProgramPayment programPayment)
        {
            var data = _context.ProgramPayments.Update(programPayment);
             _context.SaveChangesAsync();
            return data.Entity;
        }
        public ProgramPayment CreateProgramPayment(ProgramPayment programPayment)
        {
            var data = _context.ProgramPayments.Add(programPayment);
            _context.SaveChanges();
            return data.Entity;
        }
        public ProgramPayment GetProgramPayment(Guid ProgramId,Guid SubscriptionPaymentId)
        {
            var data =  _context.ProgramPayments.Where(i=>i.ProgramId==ProgramId).Where(p=>p.Status==true).FirstOrDefault(s => s.SubscriptionPaymentId == SubscriptionPaymentId);
            return data;
        }
        public ProgramPayment GetProgramPayment(Guid Id)
        {
            var data =  _context.ProgramPayments.FirstOrDefault(s => s.Id==Id);
            return data;
        }
        //program image
        public ProgramImages CreateImage(ProgramImages programImages)
        {
            var data = _context.ProgramImages.Add(programImages);
            _context.SaveChanges();
            return data.Entity;
        }
        //Subscription
        public Subscription CreateSubscription(Subscription subscription)
        {
            var data = _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
            return data.Entity;
        }
        public Subscription GetSubscription(Guid id)
        {
            var data =  _context.Subscriptions.Include(i => i.SubscriptionPayments).Where(i => i.Status == true).FirstOrDefault(i => i.Id == id);
            if (data == null)
            {
                throw new Exception("Program not found");
            }
            return data;
        } 
        public Subscription UpdateSubscription(Subscription subscription)
        {
            var data = _context.Subscriptions.Update(subscription);
             _context.SaveChangesAsync();
            return data.Entity;
        }
        public List<Subscription> GetAllSubscriptions()
        {
            var data =  _context.Subscriptions.Include(i => i.SubscriptionPayments).Where(i => i.Status == true).ToList();
            return data;
        }
        public void DeleteSubscription(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
            _context.SaveChanges();
        }
        //Subscription payments
        public SubscriptionPayment CreateSubscriptionPayment(SubscriptionPayment subscriptionPayment)
        {
            var data = _context.SubscriptionPayments.Add(subscriptionPayment);
            _context.SaveChanges();
            return data.Entity;
        }
        public SubscriptionPayment UpdateSubscritionPayment(SubscriptionPayment subscriptionPayment)
        {
            var data = _context.SubscriptionPayments.Update(subscriptionPayment);
            _context.SaveChanges();
            return data.Entity;
        }
        public List<SubscriptionPayment> GetSubscriptionAllPayments(Guid id)
        {
            var data = _context.SubscriptionPayments.Include(i => i.Subscription).Where(i => i.Status == true).Where(i => i.SubscriptionId == id).ToList();
            return data;
        }
        public SubscriptionPayment GetSubscriptionPayment (Guid id,Guid subId)
        {
            var data=_context.SubscriptionPayments.Include(i=>i.Subscription).Where(j=>j.SubscriptionId==subId).FirstOrDefault(i=>i.Id==id);
            return data;
        }
        public SubscriptionPayment GetSubscriptionPayment(Guid id)
        {
            var data = _context.SubscriptionPayments.Include(i => i.Subscription).FirstOrDefault(i => i.Id == id);
            return data;
        }

        public void DeleteSubscriptionPayment(SubscriptionPayment subscriptionPayment)
        {
            if (subscriptionPayment is null)
            {
                throw new ArgumentNullException(nameof(subscriptionPayment));
            }

            _context.SubscriptionPayments.Remove(subscriptionPayment);
            _context.SaveChanges();
        }
        //Subscribed program
        public SubscribedProgram AddSubscribedProgram(SubscribedProgram subscribedProgram)
        {
            var data=_context.SubscribedPrograms.Add(subscribedProgram);
            _context.SaveChanges();
            return data.Entity;
        }
        public List<SubscribedProgram> GetAllSubscribedPrograms()
        {
            var data=_context.SubscribedPrograms.Where(i=>i.Status == true).ToList();
            return data;
        }
        public SubscribedProgram GetSingleSubscribedProgram(Guid subscribeId,Guid programId)
        {
            var data = _context.SubscribedPrograms.Where(i => i.Status == true).FirstOrDefault(j=>j.SubscribeId==subscribeId && j.ProgramId==programId);
            return data;
        }
        public List<SubscribedProgram> GetProgramsOfSubscription(Guid subscriptionId)
        {
            var data = _context.SubscribedPrograms.Where(i => i.Status == true && i.SubscribeId== subscriptionId).ToList();
            return data;
        }
        public List<SubscribedProgram> GetSubscriptionsOfSingleProgram(Guid programId)
        {
            var data = _context.SubscribedPrograms.Where(i => i.Status == true && i.ProgramId==programId).ToList();
            return data;
        }
        public SubscribedProgram UpdateSubscribedProgram(SubscribedProgram subscribedProgram)
        {
            var data=_context.SubscribedPrograms.Update(subscribedProgram);
            _context.SaveChanges();
            return data.Entity;
        }

    }
}
