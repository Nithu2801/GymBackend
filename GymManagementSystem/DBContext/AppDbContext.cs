using GymManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberMessage> MemberMessages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
        public DbSet<ProgramPayment> ProgramPayments { get; set; }
        public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }
        public DbSet<ProgramImages> ProgramImages { get; set; }
        public DbSet<VisitorMessage> VisitorMessages { get; set; }
        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<SubscribedProgram> SubscribedPrograms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Member>().HasOne(a => a.User).WithOne(u => u.Member).HasForeignKey<Member>(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
          
            modelBuilder.Entity<Enrollment>().HasKey(e => new { e.MemberId, e.ProgramId });
            modelBuilder.Entity<SubscribedProgram>().HasKey(e => new { e.SubscribeId, e.ProgramId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
